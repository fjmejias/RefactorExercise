using Encoder.Interfaces;
using System;

namespace Encoder.Services
{
    public class EncodeService : IEncodeService
    {
        private readonly char[] _transcode;

        public EncodeService()
        {
            _transcode = InitTranscode();
        }

        private char[] InitTranscode()
        {                    
            var transcode = new char[65];
            for (int i = 0; i < 64; i++)  
            {    
                transcode[i] = (char)('A' + i);
                if (i > 25)
                    transcode[i] = (char)(transcode[i] + 6);

                if (i > 51)
                    transcode[i] = (char)(transcode[i] - 0x4b);
            }
            transcode[62] = '+';
            transcode[63] = '/';            
            transcode[64] = '=';

            return transcode;
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
                    output[c++] = _transcode[pivot];
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
                    output[c] = _transcode[reflex];
                    break;
                case 2:
                    reflex <<= 2;
                    output[c] = _transcode[reflex];
                    break;
            }

            return new string( output );
        }

        public string Decode(string input)
        {
            int l = input.Length;
            int cb = (l/4 + (Convert.ToBoolean(l%4)?1:0))*3+1;   
            char[] output = new char[cb];        
            int c = 0;
            int bits = 0;
            int reflex = 0;
            for (int j = 0; j < l; j++)
            {
                reflex <<= 6;
                bits += 6;
                bool fTerminate = '=' == input[j];
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
            
            return new string( output );                    
        }

        private int IndexOf(char ch)
        {
            int index;
            for (index = 0; index < _transcode.Length; index++)
                if (ch == _transcode[index])
                    break;    
            return index;
        }
    }
}
