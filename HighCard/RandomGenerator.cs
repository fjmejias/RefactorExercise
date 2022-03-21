using HighCard.Interfaces;
using System;

namespace HighCard
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random _rnd;

        public RandomGenerator()
        {
            _rnd = new Random(DateTime.Now.Millisecond);
        }

        public int Next(int maxValue)
        {
            return _rnd.Next(maxValue);
        }
    }
}
