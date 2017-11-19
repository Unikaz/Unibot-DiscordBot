using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using Discord;
using Discord.WebSocket;
using DiscordBot.DataModel;

namespace DiscordBot.Challenges
{
    public class Challenge
    {
        private int _id;
        private string _type;
        private string _question;
        private string _answer;
        private DateTime _dateTime;
        private Dictionary<SocketUser, int> _winnersDictionary = new Dictionary<SocketUser, int>();

        public Challenge(int id, string type, string question, string answer, DateTime dateTime)
        {
            _id = id;
            _type = type;
            _question = question;
            _answer = answer;
            _dateTime = dateTime;
        }

        public int GetDuration()
        {
            return (int) DateTime.Now.Subtract(_dateTime).TotalMilliseconds;
        }

        public bool IsCorrect(SocketUser user, string answer)
        {
            int duration = GetDuration();
            if (_answer == answer)
            {
                if (!_winnersDictionary.ContainsKey(user))
                {
                    _winnersDictionary.Add(user, duration);
                }
                return true;
            }
            return false;
        }

        public string GetResults()
        {
            var ordered = _winnersDictionary.ToList();
            ordered.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            string[] strs = ordered.Select(entry => "- " + entry.Key.Username + " : " + entry.Value + "ms").ToArray();
            return string.Join("\n", strs);
        }

        public Embed GetResultsEmbed()
        {
            var ordered = _winnersDictionary.ToList();
            ordered.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            string[] strs = ordered.Select(entry => "- " + entry.Key.Username + " : " + entry.Value + "ms").ToArray();

            EmbedBuilder embedBuilder = new EmbedBuilder();
            embedBuilder.Color = Color.Purple;
            embedBuilder.Title = "====== Challenge #" + _id + " : " + _type + " ======";
            embedBuilder.AddField("Question: ", _question);
            embedBuilder.AddField("Réponse : ", _answer);
            Console.Out.WriteLine("1");
            embedBuilder.ThumbnailUrl = ordered[0].Key.GetAvatarUrl();
            Console.Out.WriteLine("2");
            string classement = "";
            for (var i = 0; i < strs.Length; i++)
            {
                Console.Out.WriteLine("3");
                classement += (i + 1) + " " + strs[i] + "\n";
            }
            Console.Out.WriteLine("4");
            embedBuilder.AddField("Classement ", classement);
            return embedBuilder.Build();
        }

        public string GetQuestion()
        {
            return _question;
        }

        public int GetID()
        {
            return _id;
        }
    }
}