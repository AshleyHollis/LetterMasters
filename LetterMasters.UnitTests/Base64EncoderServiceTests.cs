using System;
using LetterMasters.Common;
using LetterMasters.Features.Encode;
using Shouldly;
using Xunit;

namespace LetterMasters.UnitTests
{
    public class Base64EncoderServiceTests
    {
        [Fact]
        public void Encode_SingleAlphabeticWord_IsCorrect()
        {
            const string sut = "ABC";
            const string expected = "QUJD";
            var base64EncoderService = new Base64EncoderService();

            var result = base64EncoderService.Encode(sut);

            result.IsSuccess.ShouldBeTrue();
            result.Content.ShouldBe(expected);
        }

        [Fact]
        public void Encode_2WordsSeperatedBySpace_FailsWithInvalidInput()
        {
            const string sut = "ABC DEF";
            var expected = string.Empty;
            var base64EncoderService = new Base64EncoderService();

            var result = base64EncoderService.Encode(sut);

            result.IsSuccess.ShouldBeFalse();
            result.Errors.ShouldContain(a => a.Message == ErrorMessages.Base64EncoderService.MultipleWordsExceptionMessage);
            result.Errors.ShouldHaveSingleItem();
            result.Content.ShouldBe(expected);
        }

        [Fact]
        public void Encode_1WordWith1Number_FailsWithInvalidInput()
        {
            const string sut = "ABC5";
            var expected = string.Empty;
            var base64EncoderService = new Base64EncoderService();

            var result = base64EncoderService.Encode(sut);

            result.IsSuccess.ShouldBeFalse();
            result.Errors.ShouldContain(a => a.Message == ErrorMessages.Base64EncoderService.NonAlpahabeticCharactersExceptionMessage);
            result.Errors.ShouldHaveSingleItem();
            result.Content.ShouldBe(expected);
        }

        [Fact]
        public void Encode_1WordWith1SpecialCharacter_FailsWithInvalidInput()
        {
            const string sut = "ABC%";
            var expected = string.Empty;
            var base64EncoderService = new Base64EncoderService();

            var result = base64EncoderService.Encode(sut);

            result.IsSuccess.ShouldBeFalse();
            result.Errors.ShouldContain(a => a.Message == ErrorMessages.Base64EncoderService.NonAlpahabeticCharactersExceptionMessage);
            result.Errors.ShouldHaveSingleItem();
            result.Content.ShouldBe(expected);
        }

        [Fact]
        public void Encode_1WordWith1TabCharacter_FailsWithInvalidInputAndMultipleWords()
        {
            var sut = $"ABC{(char) ConsoleKey.Tab}DEF";
            var expected = string.Empty;
            var base64EncoderService = new Base64EncoderService();

            var result = base64EncoderService.Encode(sut);

            result.IsSuccess.ShouldBeFalse();
            result.Errors.ShouldContain(a => a.Message == ErrorMessages.Base64EncoderService.MultipleWordsExceptionMessage);
            result.Errors.ShouldContain(a => a.Message == ErrorMessages.Base64EncoderService.NonAlpahabeticCharactersExceptionMessage);
            result.Errors.Count.ShouldBe(2);
            result.Content.ShouldBe(expected);
        }

        [Fact]
        public void Encode_2WordsSeperatedBySpaceAnd1NonAlphabeticCharacter_FailsWithInvalidInputAndMultipleWords()
        {
            const string sut = "ABC DEF5";
            var expected = string.Empty;
            var base64EncoderService = new Base64EncoderService();

            var result = base64EncoderService.Encode(sut);

            result.IsSuccess.ShouldBeFalse();
            result.Errors.ShouldContain(a => a.Message == ErrorMessages.Base64EncoderService.MultipleWordsExceptionMessage);
            result.Errors.ShouldContain(a => a.Message == ErrorMessages.Base64EncoderService.NonAlpahabeticCharactersExceptionMessage);
            result.Errors.Count.ShouldBe(2);
            result.Content.ShouldBe(expected);
        }
    }
}
