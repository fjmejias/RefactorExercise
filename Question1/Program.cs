using System;

namespace Question1
{
    class Program
    {
        private static readonly char[] Transcode = new char[65];

        private static void Prep()
        {
            for (int i = 0; i < 64; i++)
            {
                Transcode[i] = (char)('A' + i);
                if (i > 25) Transcode[i] = (char)(Transcode[i] + 6);
                if (i > 51) Transcode[i] = (char)(Transcode[i] - 0x4b);
            }

            Transcode[62] = '+';
            Transcode[63] = '/';
            Transcode[64] = '=';
        }

        static void Main(string[] args)
        {
            Prep();

            string test_string = "This is a test string";

            if (Convert.ToBoolean(string.CompareOrdinal(test_string, Decode(Encode(test_string)))))
            {
                Console.WriteLine("Test succeeded");
            }
            else
            {
                Console.WriteLine("Test failed");    
            }
        }


        private static string Encode(string input)
        {
            int l = input.Length;
            int cb = (l / 3 + (Convert.ToBoolean(l % 3) ? 1 : 0)) * 4;

            char[] output = new char[cb];
            for (int i = 0; i < cb; i++)
            {
                output[i] = '=';
            }

            int c = 0;
            int reflex = 0;
            const int s = 0x3f;

            for (int j = 0; j < l; j++)
            {
                reflex <<= 8;
                reflex &= 0x00ffff00;
                reflex += input[j];

                int x = ((j % 3) + 1) * 2;
                int mask = s << x;
                while (mask >= s)
                {
                    int pivot = (reflex & mask) >> x;
                    output[c++] = Transcode[pivot];
                    int invert = ~mask;
                    reflex &= invert;
                    mask >>= 6;
                    x -= 6;
                }
            }

            switch (l % 3)
            {
                case 1:
                    reflex <<= 4;
                    output[c] = Transcode[reflex];
                    break;
                case 2:
                    reflex <<= 2;
                    output[c] = Transcode[reflex];
                    break;
            }

            Console.WriteLine("{0} --> {1}\n", input, new string(output));
            return new string(output);
        }


        private static string Decode(string input)
        {
            int l = input.Length;
            int cb = (l / 4 + ((Convert.ToBoolean(l % 4)) ? 1 : 0)) * 3 + 1;
            char[] output = new char[cb];
            int c = 0;
            int bits = 0;
            int reflex = 0;
            for (int j = 0; j < l; j++)
            {
                reflex <<= 6;
                bits += 6;
                bool fTerminate = ('=' == input[j]);
                if (!fTerminate)
                    reflex += IndexOf(input[j]);

                while (bits >= 8)
                {
                    int mask = 0x000000ff << (bits % 8);
                    output[c++] = (char)((reflex & mask) >> (bits % 8));
                    int invert = ~mask;
                    reflex &= invert;
                    bits -= 8;
                }

                if (fTerminate)
                    break;
            }

            Console.WriteLine("{0} --> {1}\n", input, new string(output));
            return new string(output);
        }

        private static int IndexOf(char ch)
        {
            int index;
            for (index = 0; index < Transcode.Length; index++)
                if (ch == Transcode[index])
                    break;
            return index;
        }
    }
}