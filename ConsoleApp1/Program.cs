using System;

namespace Question2
{
    class Program
    {
        static void Main(string[] args)
        {
            HighCard card = new HighCard();
            Console.WriteLine(card.Play() ? "win" : "lose");
        }
    }
}
