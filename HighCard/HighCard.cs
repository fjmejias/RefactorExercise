using HighCard.Enums;
using HighCard.Interfaces;
using HighCard.Models;
using System;

namespace HighCard
{
    public class HighCard : IHighCard
    {
        private readonly ICardSelector _cardSelector;
        private readonly Game _game;

        public HighCard(ICardSelector cardSelector)
        {
            _cardSelector = cardSelector ?? throw new ArgumentNullException(nameof(cardSelector));
            _game = CreateNewGame();
        }

        public Game Play()
        {
            _game.PlayerA = CreatePlayer("Player A");
            _game.PlayerB = CreatePlayer("Player B");

            if (CanPlay(_game.PlayerA, _game.PlayerB))
            {
                RunGame();
            }
            else
            {
                _game.GameResult = GameResult.Error;
            }

            return _game;
        }

        #region private methods

        private Game CreateNewGame()
        {
            var game = new Game
            {
                Date = DateTime.Now,
                GameResult = GameResult.NotPlayed
            };

            _cardSelector.InitializeCards();

            return game;
        }

        private Player CreatePlayer(string name)
        {
            return new Player
            {
                Name = name,
                PlayingCard = _cardSelector.DrawCard()
            };
        }

        private void RunGame()
        {
            _game.GameResult = GameResult.Tie;
            var playerA = _game.PlayerA;
            var playerB = _game.PlayerB;

            if (playerA.PlayingCard.Number != playerB.PlayingCard.Number)
            {
                _game.GameResult = GameResult.PlayerWins;
                playerA.Winner = playerA.PlayingCard.Number > playerB.PlayingCard.Number;
                playerB.Winner = playerA.PlayingCard.Number < playerB.PlayingCard.Number;
            }
            else if (playerA.PlayingCard.Suit != playerB.PlayingCard.Suit)
            {
                _game.GameResult = GameResult.PlayerWins;
                playerA.Winner = playerA.PlayingCard.Suit > playerB.PlayingCard.Suit;
                playerB.Winner = playerA.PlayingCard.Suit < playerB.PlayingCard.Suit;
            }
        }

        private bool CanPlay(Player playerA, Player playerB)
        {
            return playerA?.PlayingCard != null && playerB?.PlayingCard != null;
        }

        #endregion
    }
}
