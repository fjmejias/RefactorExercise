using HighCard.Enums;
using HighCard.Models;

namespace HighCard.Interfaces
{
    public interface IHighCardGame
    {
        Player FirstPlayer { get; }
        Player SecondPlayer { get; }
        GameResult GameResult { get; }

        void AddPlayers(string firstPlayerName, string secondPlayerName);
        void Play();
    }
}
