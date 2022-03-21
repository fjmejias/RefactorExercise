using HighCard.Enums;
using System;

namespace HighCard.Models
{
    public class Card
    {
        public static int SuitsNumber => Enum.GetNames(typeof(Suits)).Length;

        public int Number { get; set; }
        public Suits Suit { get; set; }
    }
}
