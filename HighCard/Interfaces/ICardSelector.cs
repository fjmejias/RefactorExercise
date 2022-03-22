using HighCard.Models;

namespace HighCard.Interfaces
{
    public interface ICardSelector
    {
        int InitializeCards();
        Card DrawCard();
    }
}
