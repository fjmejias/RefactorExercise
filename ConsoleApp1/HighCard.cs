using System;

namespace Question2
{
    public class HighCard
    {
        public HighCard()
        {
        }

        public bool Play()
        {
            Random rnd = new Random();

            int i = rnd.Next() % 52 + 1;
            int j = rnd.Next() % 52 + 1;

            return i < j;
        }
    }
}
