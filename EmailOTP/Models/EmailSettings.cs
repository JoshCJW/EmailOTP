namespace EmailOTP.Models
{
    public class EmailSettings
    {
        public string SMPTHost { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPUsername { get; set; }
        public string SMTPPassword { get; set; }
    }
}
