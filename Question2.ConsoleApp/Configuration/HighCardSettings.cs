using HighCard.Contracts.Configuration;

namespace Question2.ConsoleApp.Configuration
{
    public class HighCardSettings : ICardGameSettings
    {
        // These settings can be updated from a config json file
        public int NumDecks => 2;
        public int NumCardsPerSuit => 20;
        public bool EnableJoker => true;
    }
}
