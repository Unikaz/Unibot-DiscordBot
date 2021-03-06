﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using Discord;
using Discord.WebSocket;
using DiscordBot.DataModel;
using Microsoft.VisualBasic;

namespace DiscordBot.Challenge
{
    public abstract class AChallenge
    {
        private int _id;
        private DateTime _dateTime;
        private Dictionary<SocketUser, int> _winnersDictionary = new Dictionary<SocketUser, int>();

        public AChallenge(int id)
        {
            _id = id;
            _dateTime = DateAndTime.Now;
        }

        public int GetDuration()
        {
            return (int) DateTime.Now.Subtract(_dateTime).TotalMilliseconds;
        }

        public bool IsCorrect(SocketUser user, string answer)
        {
            int duration = GetDuration();
            if (TestAnswer(answer))
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

        // create the default result embed, and call the for a potential modification by the specific challenge
        public Embed GetResultsEmbed()
        {
            var ordered = _winnersDictionary.ToList();
            ordered.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            string[] strs = ordered.Select(entry => "- " + entry.Key.Username + " : " + entry.Value + "ms").ToArray();

            EmbedBuilder embedBuilder = new EmbedBuilder();
            embedBuilder.Color = Color.Purple;
            embedBuilder.Title = "====== Challenge #" + _id + " : " + GetName() + " ======";
            if (GetQuestion() != null && GetQuestion().Length < 1024)
                embedBuilder.AddField("Question: ", GetQuestion());
            if (GetBestAnswer() != null && GetBestAnswer().Length < 1024)
                embedBuilder.AddField("Réponse : ", GetBestAnswer());
            if (ordered.Count > 0)
            {
                embedBuilder.ThumbnailUrl = ordered[0].Key.GetAvatarUrl();
                string classement = "";
                for (var i = 0; i < strs.Length; i++)
                {
                    classement += (i + 1) + " " + strs[i] + "\n";
                }
                embedBuilder.AddField("Classement ", classement);
            }
            // call to the specfic modifications
            embedBuilder = GetResultEmbedBuilder(embedBuilder);
            return embedBuilder.Build();
        }


        public int GetId()
        {
            return _id;
        }


        // Abstracts
        public abstract string GetName();

        public abstract string GetQuestion();
        public abstract bool TestAnswer(string answer);
        public abstract string GetBestAnswer();
        public abstract EmbedBuilder GetQuestionEmbedBuilder();
        public abstract EmbedBuilder GetResultEmbedBuilder(EmbedBuilder embedBuilder);
    }
}