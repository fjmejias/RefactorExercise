using Encoder.Interfaces;
using Encoder.Services;
using System;
using Unity;

namespace Question1.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string test_string = "This is a test string";

            var encoder = InitContainer().Resolve<IEncodeService>();

            var encodedText = encoder.Encode(test_string);
            Console.WriteLine("{0} --> {1}\n", test_string, new string(encodedText));

            var decodedText = encoder.Decode(encodedText);
            Console.WriteLine("{0} --> {1}\n", encodedText, new string(decodedText));

            Console.WriteLine(string.CompareOrdinal(test_string, decodedText) == 0 ? "Test succeeded" : "Test failed");
        }

        private static UnityContainer InitContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IEncodeService, EncodeService>();

            return container;
        }
    }
}