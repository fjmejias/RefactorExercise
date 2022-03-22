using HighCard;
using HighCard.Configuration;
using HighCard.Enums;
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
            IHighCardGame highCardGame = RegisterHighCard().Resolve<IHighCardGame>();
            Console.WriteLine($"HIGHCARD Game - Date: {highCardGame.GameDate}\n");

            Console.WriteLine("Enter first player name...");
            var firstPlayer = Console.ReadLine();
            Console.WriteLine("\nEnter second player name...");
            var secondPlayer = Console.ReadLine();
            Console.WriteLine();

            highCardGame.AddPlayers(firstPlayer, secondPlayer);
            highCardGame.PlayCards();

            PrintPlayersData(highCardGame);

            while (highCardGame.GameResult == GameResult.Tie)
            {
                Console.WriteLine("Tied game, press any key to draw again both players...");
                Console.ReadKey();
                Console.WriteLine();

                highCardGame.PlayCards();
                PrintPlayersData(highCardGame);
            }
            Console.WriteLine(PrintGameResult(highCardGame));
        }

        private static IUnityContainer RegisterHighCard()
        {
            var container = new UnityContainer();
            var settings = new HighCardSettings
            {
                NumCardsPerSuit = 20,
                NumDecks = 2,
                EnableJoker = true
            };

            container.RegisterInstance<IHighCardSettings>(settings);
            container.RegisterType<ICardSelector, CardSelector>();
            container.RegisterType<IHighCardGame, HighCardGame>();

            return container;
        }

        private static void PrintPlayersData(IHighCardGame game)
        {
            Console.WriteLine(PrintPlayerCard(game.FirstPlayer));
            Console.WriteLine(PrintPlayerCard(game.SecondPlayer));
            Console.WriteLine();
        }

        private static string PrintPlayerCard(Player player)
        {
            return $"{player.Name} - Card: {player.PlayingCard.Number} of {player.PlayingCard.Suit}";
        }

        private static string PrintGameResult(IHighCardGame game)
        {
            var result = string.Empty;

            switch (game.GameResult)
            {
                case GameResult.PlayerWins:
                    result = $"The winner is: {(game.FirstPlayer.Winner ? game.FirstPlayer.Name : game.SecondPlayer.Name)}";
                    break;
                case GameResult.Tie:
                    result = "Tied game";
                    break;
                case GameResult.Error:
                    result = "There was an error";
                    break;
            }

            return result;
        }
    }
}
