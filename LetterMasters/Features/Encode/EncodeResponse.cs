using System;
using LetterMasters.Common;

namespace LetterMasters.Features.Encode
{
    public class EncodeResponse: Response<String>
    {
        public bool IsInputValid { get; set; }
    }
}