using System;
using System.Linq;
using Discord;
using DiscordBot.Challenges;

namespace DiscordBot.Challenge.Challenges
{
    public class Pathfinder : AChallenge
    {
        private string _question;
        private string _bestAnswer;

        //config
        int sizeX = 60;
        int sizeY = 20;
        int rate = 25;
        char wall = '█';
        char open = '░';
        char path = '╬';

        public Pathfinder(int id) : base(id)
        {
            Random random = new Random();
            try
            {
                _question += '\n';
                for (var y = 0; y < sizeY; y++)
                {
                    for (var x = 0; x < sizeX; x++)
                    {
                        if (x == 0 && y == 0 || x == sizeX - 1 && y == sizeY - 1)
                        {
                            _question += open;
                            continue; // pour ne pas recouvrir la case de départ
                        }
                        if (random.Next(0, 100) < rate)
                            _question += wall;
                        else
                            _question += open;
                        if (x == sizeX - 1)
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
            return _question;
        }

        public override bool TestAnswer(string answer)
        {
            if (true) // si c'est bon
            {
                int score = answer.Count(chara => chara == path);
                int bestScore = Int32.MaxValue;
                if (_bestAnswer != null)
                    bestScore = _bestAnswer.Count(chara => chara == path);
                if (bestScore < score)
                    _bestAnswer = answer;

                return true;
            }
            return false;
        }

        public override string GetBestAnswer()
        {
            return _bestAnswer;
        }


        public override EmbedBuilder GetQuestionEmbedBuilder()
        {
            EmbedBuilder embedBuilder = new EmbedBuilder();
            embedBuilder.Description = "``` " + _question.Replace(open, ' ') + " ```";
            embedBuilder.AddField("Notes: ", "Vous devez indiquer votre trajet avec des " + path + "\n" +
                                             "Le départ se situe en haut à gauche et l'arrivée en bas à droite");
            return embedBuilder;
        }

        public override EmbedBuilder GetResultEmbedBuilder(EmbedBuilder embedBuilder)
        {
            embedBuilder.AddField("", "``` " + _question + " ```");
            return embedBuilder;
        }
    }
}