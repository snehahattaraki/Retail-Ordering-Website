namespace RetailOrdering.Infrastructure.Services
{
    public class EmailService
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            return Task.CompletedTask;
        }
    }
}
