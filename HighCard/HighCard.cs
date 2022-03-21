using HighCard.Enums;
using HighCard.Interfaces;
using HighCard.Models;
using System;

namespace HighCard
{
    public class HighCard : IHighCard
    {
        private const int NumCards = 52;
        private readonly IRandomGenerator _randomGenerator;

        public HighCard(IRandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator ?? throw new ArgumentNullException(nameof(randomGenerator));
        }

        public Game Play()
        {
            var game = CreateNewGame();

            if (CanPlay(game.PlayerA, game.PlayerB))
            {
                RunGame(game);
            }
            else
            {
                game.GameResult = GameResult.Error;
            }

            return game;
        }

        private Game CreateNewGame()
        {
            return new Game
            {
                Date = DateTime.Now,
                GameResult = GameResult.NotPlayed,
                PlayerA = CreatePlayer("Player A"),
                PlayerB = CreatePlayer("Player B")
            };
        }

        private Player CreatePlayer(string name)
        {
            return new Player
            {
                Name = name,
                PlayingCard = new Card
                {
                    Number = _randomGenerator.Next(NumCards) + 1,
                    Suit = (Suits)_randomGenerator.Next(Card.SuitsNumber)
                }
            };
        }

        public void RunGame(Game game)
        {
            game.GameResult = GameResult.Tie;
            var playerA = game.PlayerA;
            var playerB = game.PlayerB;

            if (playerA.PlayingCard.Number != playerB.PlayingCard.Number)
            {
                game.GameResult = GameResult.PlayerWins;
                playerA.Winner = playerA.PlayingCard.Number > playerB.PlayingCard.Number;
                playerB.Winner = playerA.PlayingCard.Number < playerB.PlayingCard.Number;
            }
            else if(playerA.PlayingCard.Suit != playerB.PlayingCard.Suit)
            {
                game.GameResult = GameResult.PlayerWins;
                playerA.Winner = playerA.PlayingCard.Suit > playerB.PlayingCard.Suit;
                playerB.Winner = playerA.PlayingCard.Suit < playerB.PlayingCard.Suit;
            }
        }

        private bool CanPlay(Player playerA, Player playerB)
        {
            return playerA?.PlayingCard != null && playerB?.PlayingCard != null;
        }
    }
}
