using HighCard.Enums;
using HighCard.Models;

namespace HighCard.Interfaces
{
    public interface IHighCardGame
    {
        Player PlayerA { get; }
        Player PlayerB { get; }
        GameResult GameResult { get; }

        void Play();

    }
}
