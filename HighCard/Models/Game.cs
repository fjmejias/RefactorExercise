using HighCard.Enums;
using System;

namespace HighCard.Models
{
    public class Game
    {
        public DateTime Date { get; set; }
        public Player PlayerA { get; set; }
        public Player PlayerB { get; set; }
        public GameResult GameResult { get; set; }
    }
}
