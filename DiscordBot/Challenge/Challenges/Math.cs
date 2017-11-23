using System;
using Discord;
using DiscordBot.Challenges;

namespace DiscordBot.Challenge.Challenges
{
    public class Math : AChallenge
    {
        private string _question;
        private string _answer;
        
        public Math(int id) : base(id)
        {
            Random random = new Random();
            int a = random.Next(1, 1000);
            int b = random.Next(1, 1000);
            float res = 0;
            int op = random.Next(0, 4);
            string operandes = " ";
            op = 3;
            switch (op)
            {
                case 0:
                    operandes = "+";
                    res = a + b;
                    break;
                case 1:
                    operandes = "-";
                    res = a - b;
                    break;
                case 2:
                    operandes = "*";
                    res = a * b;
                    break;
                case 3:
                    operandes = "/";
                    res = a / (float) b;
                    res = (int) (res * 100) / 100f;
                    break;
            }
            _question = a + operandes + b;
            _answer = res+"";
            _answer = _answer.Replace(",", ".");
        }

        public override string getName()
        {
            return "math";
        }

        public override string GetQuestion()
        {
            return _question;
        }

        public override bool TestAnswer(string answer)
        {
            return answer == _answer;
        }

        public override string GetBestAnswer()
        {
            return _answer;
        }


        public override EmbedBuilder GetQuestionEmbedBuilder()
        {
            return null;
        }

        public override EmbedBuilder GetResultEmbedBuilder(EmbedBuilder embedBuilder)
        {
            return embedBuilder;
        }
    }
}