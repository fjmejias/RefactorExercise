using HighCard.Enums;
using HighCard.Models;
using System;

namespace HighCard.Interfaces
{
    public interface IHighCardGame
    {
        Player FirstPlayer { get; }
        Player SecondPlayer { get; }
        GameResult GameResult { get; }
        DateTime GameDate { get; }

        void AddPlayers(string firstPlayerName, string secondPlayerName);
        void PlayCards();
    }
}
