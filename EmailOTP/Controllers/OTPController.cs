using EmailOTP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailOTP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OTPController : ControllerBase
    {
        private readonly IOTPService _otpService;
        public OTPController(IOTPService otpService)
        {
            _otpService = otpService;
        }

        // Generate and send OTP to the email
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateOTP([FromBody] string email)
        {
            var otp = await _otpService.GenerateNSendOTP(email);
            return Ok(new { Message = "OTP sent successfully" });
        }



        // Validate the OTP
        [HttpPost("validate")]
        public IActionResult ValidateOTP([FromBody] OTPValidationRequest request)
        {
            var isValid = _otpService.ValidateOTP(request.Email, request.OTP);

            if (isValid)
                return Ok(new { Message = "OTP is valid" });
            else
                return BadRequest(new { Message = "Invalid OTP" });
        }

    }


    // DTO for OTP validation request
    public class OTPValidationRequest
    {
        public string Email { get; set; }
        public string OTP { get; set; }
    }
}
