using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LetterMasters.Common;

namespace LetterMasters.Helpers
{
    public class RegexHelper
    {
        public static Response HasWhiteSpaceCharacters(string input)
        {
            var response = new Response();
            var getWhiteSpaceCharactersResponse = GetWhiteSpaceCharacters(input);

            response.IsSuccess = !getWhiteSpaceCharactersResponse.Content.Any();
            return response;
        }

        private static Response<List<string>> GetWhiteSpaceCharacters(string input)
        {
            var response = new Response<List<string>>();
            const string pattern = @"\s";

            var matches = GetDistinctMatches(input, pattern);
            
            response.Content = matches;
            response.IsSuccess = true;
            return response;
        }

        public static Response HasNonAlpahabeticCharacters(string input)
        {
            var response = new Response();
            var getNonAlphabeticCharactersResponse = GetNonAlphabeticCharacters(input);

            response.IsSuccess = !getNonAlphabeticCharactersResponse.Content.Any();
            return response;
        }

        private static Response<List<string>> GetNonAlphabeticCharacters(string input)
        {
            var response = new Response<List<string>>();
            const string pattern = @"[^a-zA-Z]";

            var matches = GetDistinctMatches(input, pattern);

            response.Content = matches;
            response.IsSuccess = true;
            return response;
        }

        private static List<string> GetDistinctMatches(string input, string pattern)
        {
            var matches = Regex.Matches(input, pattern);
            var distinctMatches = matches.Select(a => a.Value).Distinct().ToList();
            return distinctMatches;
        }
    }
}