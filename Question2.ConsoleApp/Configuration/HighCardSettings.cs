using HighCard.Contracts.Configuration;

namespace Question2.ConsoleApp.Configuration
{
    public class HighCardSettings : ICardGameSettings
    {
        public int NumDecks { get; set; }
        public int NumCardsPerSuit { get; set; }
        public bool EnableJoker { get; set; }
    }
}
