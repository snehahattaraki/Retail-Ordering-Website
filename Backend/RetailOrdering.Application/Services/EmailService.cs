using RetailOrdering.Application.DTOs.Email;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class EmailService : IEmailService
    {
        public Task<EmailResponseDto> SendEmailAsync(EmailRequestDto emailRequestDto) => Task.FromResult(new EmailResponseDto { IsSent = true, Message = "Email queued." });
    }
}
