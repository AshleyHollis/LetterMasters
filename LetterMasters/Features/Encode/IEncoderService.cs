namespace LetterMasters.Features.Encode
{
    public interface IEncoderService
    {
        EncodeResponse Encode(string input);
    }
}