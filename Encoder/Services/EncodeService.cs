using Encoder.Interfaces;
using System;

namespace Encoder.Services
{
    public class EncodeService : IEncodeService
    {
        private readonly char[] _transCode;

        public EncodeService()
        {
            _transCode = InitTransCode();
        }

        private char[] InitTransCode()
        {                    
            var transCode = new char[65];
            for (int i = 0; i < 64; i++)  
            {    
                transCode[i] = (char)('A' + i);
                if (i > 25)
                    transCode[i] = (char)(transCode[i] + 6);

                if (i > 51)
                    transCode[i] = (char)(transCode[i] - 0x4b);
            }
            transCode[62] = '+';
            transCode[63] = '/';
            transCode[64] = '=';

            return transCode;
        }

        public string Encode(string input)
        {
            int l = input.Length;
            int cb = (l / 3 + (Convert.ToBoolean(l % 3) ? 1 : 0)) * 4;

            char[] output = new char[cb];
            for ( int i = 0; i < cb; i++ )
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
 
                int x = (j%3+1)*2;          
                int mask = s << x;
                while (mask >= s)
                {
                    int pivot =  (reflex & mask) >> x;
                    output[c++] = _transCode[pivot];
                    int invert = ~mask;
                    reflex &= invert;
                    mask >>= 6;
                    x -= 6;
                }
            }
 
            switch (l%3)
            {
                case 1:
                    reflex <<= 4;
                    output[c] = _transCode[reflex];
                    break;
                case 2:
                    reflex <<= 2;
                    output[c] = _transCode[reflex];
                    break;
            }

            return new string(output);
        }

        public string Decode(string input)
        {
            int l = input.Length;
            int cb = (l/4 + (Convert.ToBoolean(l%4)?1:0))*3+1;   
            char[] output = new char[cb];        
            int c = 0;
            int bits = 0;
            int reflex = 0;
            bool fTerminate = false;
            for (int j = 0; j < l && !fTerminate; j++)
            {
                reflex <<= 6;
                bits += 6;
                fTerminate = '=' == input[j];
                if (!fTerminate)
                    reflex += Array.IndexOf(_transCode, input[j]);
    
                while (bits >= 8)
                {
                    int mask = 0x000000ff << (bits % 8);                                        
                    output[c++] = (char)((reflex & mask) >> (bits % 8));    
                    int invert = ~mask;
                    reflex &= invert;
                    bits -= 8;
                }
            }

            return new string(output);
        }
    }
}
