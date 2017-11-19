using System;
using System.Collections.Generic;

namespace DiscordBot.Challenges
{
    public class ChallengesManager
    {
        private static ChallengesManager _instance = null;

        private ChallengesManager()
        {
        }

        public static ChallengesManager Get => _instance ?? (_instance = new ChallengesManager());

        //=======================================================

        private int _challengeIndex = 0;
        Dictionary<int, Challenge> _challenges = new Dictionary<int, Challenge>();

        public Challenge CreateChallenge(string type, string question, string answer, DateTime dateTime)
        {
            Challenge challenge = new Challenge(++_challengeIndex, type, question, answer, dateTime);
            _challenges[_challengeIndex] = challenge;
            return challenge;
        }

        public Challenge GetChallenge(int challengeId)
        {
            Challenge challenge = null;
            _challenges.TryGetValue(challengeId, out challenge);
            return challenge;
        }
    }
}