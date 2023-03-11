namespace CityInfo.API.Services
{
    public class LocalMailService
    {
        private string _mailTo = "admin@test.com";
        private string _mailFrom = "noreply@test.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, " + $"with {nameof(LocalMailService)}.");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
