namespace EmailOTP.Services.Interfaces
{
    public interface IOTPService
    {
        Task<string> GenerateNSendOTP(string email);
        bool ValidateOTP(string email, string otp);
    }
}
