using EmailOTP.Services.Interfaces;
using System.Collections.Concurrent;

namespace EmailOTP.Services
{
    public class OTPService : IOTPService
    {
        private readonly IEmailService _emailService;


        // This is for thread safe operations
        private readonly ConcurrentDictionary<string, string> _otps = new ConcurrentDictionary<string, string>();

        public OTPService(IEmailService emailService)
        {
            _emailService = emailService;
        }

      
        public async Task<string> GenerateNSendOTP(string email)
        {
            var otp = GenerateOTP();
            _otps[email] = otp; // Store OTP with email as key

            await _emailService.SendEmail(email, "Your OTP", $"Your OTP is: {otp}");

            SetTimerToRemoveOTP(email); 

            return otp;
        }

        private void SetTimerToRemoveOTP(string email)
        {
            var timer = new Timer(RemoveOTP, email, 60000, Timeout.Infinite);
        }

        private void RemoveOTP(object state)
        {
            var email = (string)state;
            _otps.TryRemove(email, out _);
        }


        public bool ValidateOTP(string email, string otp)
        {
            if (_otps.TryGetValue(email, out var validOtp))
            {
                return otp == validOtp;
            }
            return false;
        }

        private string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(0, 1000000).ToString("D6");
        }

    }
}
