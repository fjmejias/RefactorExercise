using HighCard.Enums;
using HighCard.Interfaces;
using HighCard.Models;
using System;

namespace HighCard
{
    public class HighCardGame : IHighCardGame
    {
        public Player FirstPlayer { get; private set; }
        public Player SecondPlayer { get; private set; }
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

        public void AddPlayers(string firstPlayerName, string secondPlayerName)
        {
            FirstPlayer = new Player { Name = firstPlayerName };
            SecondPlayer = new Player { Name = secondPlayerName };
        }

        public void Play()
        {
            DrawPlayerCard(FirstPlayer);
            DrawPlayerCard(SecondPlayer);

            if (CanPlay(FirstPlayer, SecondPlayer))
            {
                RunGame();
            }
            else
            {
                GameResult = GameResult.Error;
            }
        }

        #region private methods

        public void DrawPlayerCard(Player player)
        {
            if (player == null)
            {
                throw new Exception("The player is not created yet. You must add the players before drawing cards.");
            }

            player.PlayingCard = _cardSelector.DrawCard();
        }

        private void RunGame()
        {
            GameResult = GameResult.Tie;

            if (FirstPlayer.PlayingCard.Number != SecondPlayer.PlayingCard.Number)
            {
                GameResult = GameResult.PlayerWins;
                FirstPlayer.Winner = FirstPlayer.PlayingCard.Number > SecondPlayer.PlayingCard.Number;
                SecondPlayer.Winner = FirstPlayer.PlayingCard.Number < SecondPlayer.PlayingCard.Number;
            }
            else if (FirstPlayer.PlayingCard.Suit != SecondPlayer.PlayingCard.Suit)
            {
                GameResult = GameResult.PlayerWins;
                FirstPlayer.Winner = FirstPlayer.PlayingCard.Suit > SecondPlayer.PlayingCard.Suit;
                SecondPlayer.Winner = FirstPlayer.PlayingCard.Suit < SecondPlayer.PlayingCard.Suit;
            }
        }

        private bool CanPlay(Player PlayerA, Player playerB)
        {
            return PlayerA?.PlayingCard != null && playerB?.PlayingCard != null;
        }

        #endregion
    }
}
