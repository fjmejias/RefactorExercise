using HighCard.Contracts.Enums;
using HighCard.Contracts.Models;
using System;

namespace HighCard.Contracts.Interfaces
{
    public interface ICardGame
    {
        Player FirstPlayer { get; }
        Player SecondPlayer { get; }
        GameResult GameResult { get; }
        DateTime GameDate { get; }

        void AddPlayers(string firstPlayerName, string secondPlayerName);
        void PlayCards();
    }
}
