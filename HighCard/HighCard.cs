using HighCard.Enums;
using HighCard.Interfaces;
using System;

namespace HighCard
{
    public class HighCard : IHighCard
    {
        private const int NumCards = 52;
        private readonly IRandomGenerator _randomGenerator;

        public HighCard(IRandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator ?? throw new ArgumentNullException(nameof(randomGenerator));
        }

        public HighCardResult Play()
        {
            int playerA = _randomGenerator.Next() % NumCards + 1;
            int playerB = _randomGenerator.Next() % NumCards + 1;

            return GetResult(playerA, playerB);
        }

        private HighCardResult GetResult(int playerA, int playerB)
        {
            var result = HighCardResult.Tie;

            result = playerA < playerB ? HighCardResult.Win : HighCardResult.Lose;

            return result;
        }
    }
}
