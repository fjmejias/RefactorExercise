namespace HighCard.Configuration
{
    public class HighCardSettings : IHighCardSettings
    {
        public int NumDecks { get; set; }
        public int NumCardsPerSuit { get; set; }
        public bool EnableJoker { get; set; }
    }
}
