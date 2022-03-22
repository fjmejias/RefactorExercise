namespace HighCard.Configuration
{
    public interface IHighCardSettings
    {
        public int NumDecks { get; set; }
        public int NumCardsPerDeck { get; set; }
    }
}
