namespace HighCard.Configuration
{
    public class HighCardSettings : IHighCardSettings
    {
        public int NumDecks { get; set; }
        public int NumCardsPerDeck { get; set; }
        public bool EnableJoker { get; set; }
    }
}
