namespace LetterMasters.Common
{
    public static class ErrorMessages
    {
        public static class Base64EncoderService
        {
            public static string MultipleWordsExceptionMessage = "Multiple words can not be encoded. Please ensure that there are no spaces or other whitespace characters in the input.";
            public static string NonAlpahabeticCharactersExceptionMessage = "Non alpahabetic words can not be encoded. Please ensure that there are no characters except for alpahabetic characters in the input.";
        }
    }
}