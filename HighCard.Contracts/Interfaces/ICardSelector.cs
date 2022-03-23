using HighCard.Contracts.Models;

namespace HighCard.Contracts.Interfaces
{
    public interface ICardSelector
    {
        int InitializeCards();
        Card DrawCard();
    }
}
