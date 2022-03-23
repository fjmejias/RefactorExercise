using HighCard;
using HighCard.Contracts.Configuration;
using HighCard.Contracts.Enums;
using HighCard.Contracts.Interfaces;
using HighCard.Contracts.Models;
using Question2.ConsoleApp.Configuration;
using System;
using Unity;

namespace Question2.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = RegisterHighCard();
            ICardGame highCardGame = container.Resolve<ICardGame>();
            ICardGameSettings settings = container.Resolve<ICardGameSettings>();

            Console.WriteLine($"HIGHCARD Game - Date: {highCardGame.GameDate}\n");
            Console.Write($"Number of Cards per Suit: {settings.NumCardsPerSuit}" +
                          $"{(settings.EnableJoker ? " + one Joker" : string.Empty)}\n");
            Console.Write($"Number of Decks: {settings.NumDecks}\n");

            Console.WriteLine("\nEnter first player name...");
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
            container.RegisterSingleton<ICardGameSettings, HighCardSettings>();
            container.RegisterType<ICardSelector, CardSelector>();
            container.RegisterType<ICardGame, HighCardGame>();

            return container;
        }

        private static void PrintPlayersData(ICardGame game)
        {
            Console.WriteLine(PrintPlayerCard(game.FirstPlayer));
            Console.WriteLine(PrintPlayerCard(game.SecondPlayer));
            Console.WriteLine();
        }

        private static string PrintPlayerCard(Player player)
        {
            return $"{player.Name} - Card: {player.PlayingCard.Number} of {player.PlayingCard.Suit}";
        }

        private static string PrintGameResult(ICardGame game)
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
