﻿using System;
using System.Collections.Generic;
using System.Linq;
using DiscordBot.Challenge.Challenges;
using Math = DiscordBot.Challenge.Challenges.Math;

namespace DiscordBot.Challenge
{
    public class ChallengesManager
    {
        private static ChallengesManager _instance = null;
        private static string _prefix = "[ChallengeManager] ";

        private ChallengesManager()
        {
            _challengesList.Add("math", typeof(Math));
            _challengesList.Add("sudoku", typeof(Sudoku));
            _challengesList.Add("path", typeof(Pathfinder));
        }

        public static ChallengesManager Get => _instance ?? (_instance = new ChallengesManager());

        //=======================================================
        private Dictionary<string, Type> _challengesList = new Dictionary<string, Type>();
        private Dictionary<int, AChallenge> _challenges = new Dictionary<int, AChallenge>();
        private int _challengeIndex = 0;


        public AChallenge CreateChallenge()
        {
            Random random = new Random();
            string type = _challengesList.ElementAt(random.Next(0, _challengesList.Count)).Key;
            return CreateChallenge(type);
        }

        public AChallenge CreateChallenge(string type)
        {
            if (_challengesList.ContainsKey(type))
            {
                Console.Out.WriteLine(_prefix + "New Challenge #" + (_challengeIndex+1) + " creation...");
                AChallenge aChallenge = (AChallenge) Activator.CreateInstance(_challengesList[type], ++_challengeIndex);
                _challenges[_challengeIndex] = aChallenge;
                return aChallenge;
            }
            Console.Out.WriteLine(_prefix + "Error on ChallengesManager.CreateChallenge: type " + type + " isn't found");
            return null;
        }

        public AChallenge GetChallenge(int challengeId)
        {
            AChallenge aChallenge = null;
            _challenges.TryGetValue(challengeId, out aChallenge);
            return aChallenge;
        }
    }
}