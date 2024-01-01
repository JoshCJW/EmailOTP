namespace EmailOTP.Test
{
    using Moq;
    using Xunit;
    using EmailOTP.Services;
    using EmailOTP.Services.Interfaces;
    using EmailOTP.Models;

    public class OTPServiceTests
    {
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly OTPService _otpService;

        public OTPServiceTests()
        {
            _emailServiceMock = new Mock<IEmailService>();
            _otpService = new OTPService(_emailServiceMock.Object);
        }

        [Fact]
        public async Task GenerateOTPSendsEmail()
        {
            // Arrange
            var email = "test@example.com";

            // Act
            var otp = await _otpService.GenerateNSendOTP(email);

            // Assert
            _emailServiceMock.Verify(m => m.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.NotNull(otp);
            Assert.Equal(6, otp.Length); 
        }

        [Fact]
        public void ValidateOTPCorrectlyValidates()
        {
            // Arrange
            var email = "test@example.com";
            var otp = "556692"; 
            _otpService.GenerateNSendOTP(email).Wait(); 

            // Act
            var result = _otpService.ValidateOTP(email, otp);

            // Assert
            Assert.True(result); 
        }
    }
}