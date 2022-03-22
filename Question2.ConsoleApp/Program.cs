using HighCard;
using HighCard.Configuration;
using HighCard.Interfaces;
using HighCard.Models;
using System;
using Unity;

namespace Question2.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IHighCard highCardGame = RegisterHighCard().Resolve<IHighCard>();

            var game = highCardGame.Play();
            Console.WriteLine($"HIGHCARD Game - Date: {game.Date}");
            Console.WriteLine(PrintPlayerCard(game.PlayerA));
            Console.WriteLine(PrintPlayerCard(game.PlayerB));
            Console.WriteLine(PrintGameResult(game));
        }

        private static IUnityContainer RegisterHighCard()
        {
            var container = new UnityContainer();
            var settings = new HighCardSettings
            {
                NumCardsPerDeck = 52,
                NumDecks = 3
            };

            container.RegisterInstance(settings);
            container.RegisterType<ICardSelector, CardSelector>();
            container.RegisterType<IHighCard, HighCard.HighCard>();

            return container;
        }

        private static string PrintPlayerCard(Player player)
        {
            return$"{player.Name} - Card: {player.PlayingCard.Number} of {player.PlayingCard.Suit}";
        }

        private static string PrintGameResult(Game game)
        {
            var result = $"{game.GameResult} - {(game.PlayerA.Winner ? game.PlayerA.Name : game.PlayerB.Name)}";

            switch (game.GameResult)
            {
                case HighCard.Enums.GameResult.PlayerWins:
                    result = $"The winner is: {(game.PlayerA.Winner ? game.PlayerA.Name : game.PlayerB.Name)}";
                    break;
                case HighCard.Enums.GameResult.Tie:
                    result = "Tied game";
                    break;
                case HighCard.Enums.GameResult.Error:
                    result = "There was an error";
                    break;
            }

            if (game.GameResult == HighCard.Enums.GameResult.PlayerWins)
            {
                
            }

            return result;
        }
    }
}
