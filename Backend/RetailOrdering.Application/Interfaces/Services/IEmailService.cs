using RetailOrdering.Application.DTOs.Email;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task<EmailResponseDto> SendEmailAsync(EmailRequestDto emailRequestDto);
    }
}
