using HighCard.Interfaces;
using System;

namespace HighCard
{
    public class HighCard : IHighCard
    {
        private readonly Random _rnd;

        public HighCard()
        {
            _rnd = new Random(DateTime.Now.Millisecond);
        }

        public bool Play()
        {
            int i = _rnd.Next() % 52 + 1;
            int j = _rnd.Next() % 52 + 1;

            return i < j;
        }
    }
}
