namespace Encoder.Interfaces
{
    public interface IEncodeService
    {
        string Encode(string input);
        
        string Decode(string input);
    }
}
