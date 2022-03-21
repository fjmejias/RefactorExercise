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
            IHighCard card = RegisterHighCard().Resolve<IHighCard>();

            Console.WriteLine(card.Play().ToString());
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
