using System;
using System.Linq;
using Discord;
using DiscordBot.Challenges;

namespace DiscordBot.Challenge.Challenges
{
    public class Pathfinder : AChallenge
    {
        private readonly string _question;
        private string _bestAnswer;

        //config

        private const int SizeX = 40;
        private const int SizeY = 20;
        private const int Rate = 20;
        private const char Wall = '█';
        private const char Open = '░';
        private const char Path = '╬';

        public Pathfinder(int id) : base(id)
        {
            Random random = new Random();
            try
            {
                for (var y = 0; y < SizeY; y++)
                {
                    for (var x = 0; x < SizeX; x++)
                    {
                        if (x == 0 && y == 0 || x == SizeX - 1 && y == SizeY - 1)
                        {
                            _question += Open;
                            continue; // pour ne pas recouvrir la case de départ
                        }
                        if (random.Next(0, 100) < Rate)
                            _question += Wall;
                        else
                            _question += Open;
                        if (x == SizeX - 1)
                            _question += '\n';
                    }
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
            }

            Console.Out.WriteLine(_question);
        }

        public override string getName()
        {
            return "pathfinder";
        }

        public override string GetQuestion()
        {
            return "\n" + _question;
        }

        public override bool TestAnswer(string answer)
        {
            var distance = Valid(answer);
            if (distance <= 0) return false;
            var bestScore = int.MaxValue;
            if (_bestAnswer != null)
            {
                bestScore = _bestAnswer.Count(chara => chara == Path);
            }
            if (bestScore > distance)
            {
                _bestAnswer = answer;
            }
            return true;
        }

        public override string GetBestAnswer()
        {
            return _bestAnswer;
        }


        public override EmbedBuilder GetQuestionEmbedBuilder()
        {
            var embedBuilder = new EmbedBuilder();
            embedBuilder.AddField("Notes: ", "Vous devez indiquer votre trajet avec des `" + Path + "`\n" +
                                             "Le départ se situe en haut à gauche et l'arrivée en bas à droite");
            return embedBuilder;
        }

        public override EmbedBuilder GetResultEmbedBuilder(EmbedBuilder embedBuilder)
        {
            if (embedBuilder.Fields.Count(field => field.Value == _bestAnswer) == 0)
                embedBuilder.Description = "\n" + _bestAnswer + "";
            return embedBuilder;
        }

        private int Valid(string answer)
        {
            //first, test if the wall are still there
            int i;
            for (i = 0; i < _question.Length - 1; i++)
            {
                if (_question[i] != Wall || answer[i] == Wall) continue;
                Console.Out.WriteLine("wall destroyed at " + i + " charR is '" + answer[i] + "'");
                return -1;
            }
            // now, check if the path is valid
            var closed = new bool[SizeX, SizeY];
            var x = 0;
            var y = 0;
            for (i = 0; i < 10000000; i++)
            {
                //work on cell x,y
                closed[x, y] = true;
                // test if its the end
                if (x == SizeX - 1 && y == SizeY - 1)
                    return i;
                //test neighbours
                if (x > 0 && GetCell(answer, x - 1, y) == Path && !closed[x - 1, y])
                {
                    x -= 1;
                    continue;
                }
                if (x < SizeX - 1 && GetCell(answer, x + 1, y) == Path && !closed[x + 1, y])
                {
                    x += 1;
                    continue;
                }
                if (y > 0 && GetCell(answer, x, y - 1) == Path && !closed[x, y - 1])
                {
                    y -= 1;
                    continue;
                }
                if (y < SizeY - 1 && GetCell(answer, x, y + 1) == Path && !closed[x, y + 1])
                {
                    y += 1;
                    continue;
                }
                Console.Out.WriteLine("no other way, failed at " + x + "," + y);
                return -1;
            }
            Console.Out.WriteLine("test overflow ?");
            return -1;
        }

        private char GetCell(string grid, int x, int y)
        {
            return grid[y * (SizeX + 1) + x];
        }
    }
}