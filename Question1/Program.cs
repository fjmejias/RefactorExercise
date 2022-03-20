using Encoder.Interfaces;
using Encoder.Services;
using System;

namespace Question1
{
    class Program
    {
        static void Main(string[] args)
        {
            string test_string = "This is a test string";

            IEncodeService encoder = new EncodeService();

            var encodedText = encoder.Encode(test_string);
            Console.WriteLine("{0} --> {1}\n", test_string, new string(encodedText));

            var decodedText = encoder.Decode(encodedText);
            Console.WriteLine("{0} --> {1}\n", encodedText, new string(decodedText));

            var compare = string.CompareOrdinal(test_string, decodedText);
            if (Convert.ToBoolean(compare))
            {
                Console.WriteLine("Test succeeded");
            }
            else
            {
                Console.WriteLine("Test failed");    
            }
        }
    }
}