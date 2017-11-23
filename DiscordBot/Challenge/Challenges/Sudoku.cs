using System;
using DiscordBot.Challenges;
using Discord;

namespace DiscordBot.Challenge.Challenges
{
    public class Sudoku : AChallenge
    {
        private string _question;
        private string _answer;

        public Sudoku(int id) : base(id)
        {
//            var wc = new WebClient();
//            string webData = wc.DownloadString("http://www.sudokuweb.org/");
//            webData = WebUtility.HtmlDecode(webData);
//            HtmlDocument htmlDocument = new HtmlDocument();
//            htmlDocument.LoadHtml(webData);
//            _question = "";
//            _answer = "";
//
//            var document = htmlDocument.DocumentNode;
//            foreach (var htmlNode in document.QuerySelectorAll("#sudoku span:nth-child(1)"))
//            {
//                _question += htmlNode.GetClassList().Contains("sedy") ? htmlNode.InnerHtml : "0";
//                _answer += htmlNode.InnerHtml;
//            }

            //todo find a good API instead of a hardcode challenge ^^ 
            //simple
            _question = "700000001020006050006714200061040800007809100009030520004672300070300040600000002";
            _answer = "748523961123986457596714283361245879257869134489137526814672395972351648635498712";
            
            //easy
//            _question = "000080032020000549000900807007408001032790000090100000003010400009000000400500000";
//            _answer = "975684132628371549341952867567428391132795684894163275783219456259846713416537928";

            //intermediate
//            _question = "000000859004000001000000020160020000087100000000006700200050000010008600400092080";
//            _answer = "623714859754289361891563427169827543387145296542936718238651974915478632476392185";
            
            //debug
            Console.Out.WriteLine("question; " + _question);
            Console.Out.WriteLine(SudokuDisplay(_question));
            Console.Out.WriteLine("response; " + _answer);
            Console.Out.WriteLine(SudokuDisplay(_answer));
        }


        public static string SudokuDisplay(string str)
        {
            string res = "``` ";
            for (var i = 0; i < str.Length; i++)
            {
                if (i % 3 == 0 && i != 0 && i % 9 != 0)
                    res += "|";
                if (i % 9 == 0)
                    res += "\n";
                if (i == 3 * 9 || i == 6 * 9)
                    res += " ---------------------------\n";

                res += " " + (str[i] == '0' ? ' ' : str[i]) + " ";
            }
            return res + "\n \n```";
        }

        public static EmbedBuilder SudokuDisplayEmbed(EmbedBuilder embedBuilder, string str)
        {
            if (embedBuilder == null)
                embedBuilder = new EmbedBuilder();
            embedBuilder.AddField("```-------------- Grille --------------```", SudokuDisplay(str));
            
            return embedBuilder;
        }

        // from AChallenge
        public override string getName()
        {
            return "sudoku";
        }

        public override string GetQuestion()
        {
            return _question;
        }

        public override string GetAnswer()
        {
            return _answer;
        }

        public override EmbedBuilder GetQuestionEmbedBuilder()
        {
            return SudokuDisplayEmbed(null, _question);
        }

        public override EmbedBuilder GetResultEmbedBuilder(EmbedBuilder embedBuilder)
        {
            return SudokuDisplayEmbed(embedBuilder, _answer);
        }
    }
}