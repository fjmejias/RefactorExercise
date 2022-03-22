using HighCard.Enums;
using HighCard.Interfaces;
using HighCard.Models;
using System;

namespace HighCard
{
    public class HighCardGame : IHighCardGame
    {
        public Player PlayerA { get; private set; }
        public Player PlayerB { get; private set; }
        public GameResult GameResult { get; private set; }
        public DateTime GameDate { get; }

        private readonly ICardSelector _cardSelector;

        public HighCardGame(ICardSelector cardSelector)
        {
            _cardSelector = cardSelector ?? throw new ArgumentNullException(nameof(cardSelector));
            GameDate = DateTime.Now;
            GameResult = GameResult.NotPlayed;

            _cardSelector.InitializeCards();
        }

        public void Play()
        {
            PlayerA = CreatePlayer("Player A");
            PlayerB = CreatePlayer("Player B");

            if (CanPlay(PlayerA, PlayerB))
            {
                RunGame();
            }
            else
            {
                GameResult = GameResult.Error;
            }
        }

        #region private methods

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
            GameResult = GameResult.Tie;

            if (PlayerA.PlayingCard.Number != PlayerB.PlayingCard.Number)
            {
                GameResult = GameResult.PlayerWins;
                PlayerA.Winner = PlayerA.PlayingCard.Number > PlayerB.PlayingCard.Number;
                PlayerB.Winner = PlayerA.PlayingCard.Number < PlayerB.PlayingCard.Number;
            }
            else if (PlayerA.PlayingCard.Suit != PlayerB.PlayingCard.Suit)
            {
                GameResult = GameResult.PlayerWins;
                PlayerA.Winner = PlayerA.PlayingCard.Suit > PlayerB.PlayingCard.Suit;
                PlayerB.Winner = PlayerA.PlayingCard.Suit < PlayerB.PlayingCard.Suit;
            }
        }

        private bool CanPlay(Player PlayerA, Player playerB)
        {
            return PlayerA?.PlayingCard != null && playerB?.PlayingCard != null;
        }

        #endregion
    }
}
