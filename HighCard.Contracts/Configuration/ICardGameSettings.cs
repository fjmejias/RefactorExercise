namespace HighCard.Contracts.Configuration
{
    public interface ICardGameSettings
    {
        public int NumDecks { get; }
        public int NumCardsPerSuit { get; }
        public bool EnableJoker { get; }
    }
}
