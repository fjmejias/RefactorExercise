using HighCard.Interfaces;
using System;

namespace HighCard
{
    public class HighCard : IHighCard
    {
        private readonly IRandomGenerator _randomGenerator;

        public HighCard(IRandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator ?? throw new ArgumentNullException(nameof(randomGenerator));
        }

        public bool Play()
        {
            int playerA = _randomGenerator.Next() % 52 + 1;
            int playerB = _randomGenerator.Next() % 52 + 1;

            return playerA < playerB;
        }
    }
}
