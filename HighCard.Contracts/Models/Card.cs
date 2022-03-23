using System;
using HighCard.Contracts.Enums;

namespace HighCard.Contracts.Models
{
    public class Card
    {
        public static int SuitsNumber => Enum.GetNames(typeof(Suits)).Length;

        public int Number { get; set; }
        public Suits? Suit { get; set; }
        public bool IsJoker { get; set; }
    }
}
