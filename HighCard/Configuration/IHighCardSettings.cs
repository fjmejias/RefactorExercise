namespace HighCard.Configuration
{
    public interface IHighCardSettings
    {
        public int NumDecks { get; }
        public int NumCardsPerSuit { get; }
        public bool EnableJoker { get; }
    }
}
