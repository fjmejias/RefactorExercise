using HighCard;
using HighCard.Interfaces;
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
            Console.WriteLine($"Player 1: {game.PlayerA.Name} - Card: {game.PlayerA.PlayingCard.Number}");
            Console.WriteLine($"Player 2: {game.PlayerB.Name} - Card: {game.PlayerB.PlayingCard.Number}");
            Console.WriteLine($"{game.GameResult} - {(game.PlayerA.Winner ? game.PlayerA.Name : game.PlayerB.Name)}");
        }

        private static IUnityContainer RegisterHighCard()
        {
            var container = new UnityContainer();

            container.RegisterType<IRandomGenerator, RandomGenerator>();
            container.RegisterType<IHighCard, HighCard.HighCard>();

            return container;
        }
    }
}
