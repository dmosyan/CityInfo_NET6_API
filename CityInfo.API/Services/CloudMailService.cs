namespace CityInfo.API.Services
{
    public class CloudMailService
    {
        private string _mailTo = "admin@test.com";
        private string _mailFrom = "noreply@test.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, " + $"with {nameof(CloudMailService)}.");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
