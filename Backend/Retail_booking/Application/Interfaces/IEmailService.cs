using System.Threading.Tasks;

namespace RetailOrdering.Application.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string html);
}
