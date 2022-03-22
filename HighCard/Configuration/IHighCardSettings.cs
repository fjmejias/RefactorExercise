namespace HighCard.Configuration
{
    public interface IHighCardSettings
    {
        public int NumDecks { get; }
        public int NumCardsPerDeck { get; }
        public bool EnableJoker { get; }
    }
}
