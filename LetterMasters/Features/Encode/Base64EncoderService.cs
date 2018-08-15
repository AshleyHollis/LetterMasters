using System;
using System.Linq;
using LetterMasters.Common;
using LetterMasters.Helpers;

namespace LetterMasters.Features.Encode
{
    public class Base64EncoderService : IEncoderService
    {
        private Response Validate(string input)
        {
            var response = new Response();

            var hasWhiteSpaceCharactersResponse = RegexHelper.HasWhiteSpaceCharacters(input);
            if (!hasWhiteSpaceCharactersResponse.IsSuccess)
            {
                response.AddErrors(hasWhiteSpaceCharactersResponse);

                var multipleWordsException =
                    new ApplicationException(ErrorMessages.Base64EncoderService.MultipleWordsExceptionMessage);
                multipleWordsException.Data["Input"] = input;
                response.AddError(multipleWordsException);
            }

            var hasNonAlpahabeticCharactersResponse = RegexHelper.HasNonAlpahabeticCharacters(input);
            if (!hasNonAlpahabeticCharactersResponse.IsSuccess)
            {
                response.AddErrors(hasNonAlpahabeticCharactersResponse);

                var nonAlpahabeticCharactersException =
                    new ApplicationException(
                        ErrorMessages.Base64EncoderService.NonAlpahabeticCharactersExceptionMessage);
                nonAlpahabeticCharactersException.Data["Input"] = input;
                response.AddError(nonAlpahabeticCharactersException);
            }

            response.IsSuccess = !response.Errors.Any();
            return response;
        }

        public EncodeResponse Encode(string input)
        {
            var response = new EncodeResponse();

            var validateResponse = Validate(input);
            if (!validateResponse.IsSuccess)
            {
                response.AddErrors(validateResponse);
                return response;
            }

            var inputAsBytes = System.Text.Encoding.UTF8.GetBytes(input);
            var inputEncoded = Convert.ToBase64String(inputAsBytes);
            response.Content = inputEncoded;

            response.IsSuccess = true;
            return response;
        }
    }
}