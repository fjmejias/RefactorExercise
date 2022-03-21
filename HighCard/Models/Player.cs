namespace HighCard.Models
{
    public class Player
    {
        public string Name { get; set; }
        public Card PlayingCard { get; set; }
        public bool Winner { get; set; }
    }
}
