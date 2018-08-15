using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace LetterMasters.Features.Encode
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncodeController : ControllerBase
    {
        private IEncoderService EncoderService { get; }

        public EncodeController(IEncoderService encoderService)
        {
            EncoderService = encoderService;
        }

        [HttpGet]
        public ActionResult<string> Get([FromQuery]string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    return BadRequest(
                        "Input query string paramater was not specificy. Please specify the Input query string parmater in you're request.");
                }

                var encodeResponse = EncoderService.Encode(input);
                if (!encodeResponse.IsInputValid)
                {
                    return BadRequest(encodeResponse);
                }
                if (!encodeResponse.IsSuccess)
                {
                    return StatusCode(500, encodeResponse);
                }
                return encodeResponse.Content;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
