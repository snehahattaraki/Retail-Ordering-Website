using System.Threading.Tasks;
using RetailOrdering.Application.DTOs.Email;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessageDto message);
    }
}
