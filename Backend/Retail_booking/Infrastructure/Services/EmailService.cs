using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using RetailOrdering.Application.Interfaces;
using RetailOrdering.Infrastructure.Data;
using RetailOrdering.Domain.Entities;

namespace RetailOrdering.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;

    public EmailService(IConfiguration configuration, AppDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task SendEmailAsync(string to, string subject, string html)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_configuration["Smtp:FromName"] ?? "noreply", _configuration["Smtp:From"]));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;

        var body = new BodyBuilder { HtmlBody = html };
        message.Body = body.ToMessageBody();

        using var client = new SmtpClient();
        var host = _configuration["Smtp:Host"];
        var port = int.Parse(_configuration["Smtp:Port"] ?? "25");
        var user = _configuration["Smtp:User"];
        var pass = _configuration["Smtp:Pass"];

        await client.ConnectAsync(host, port, MailKit.Security.SecureSocketOptions.StartTls);
        if (!string.IsNullOrEmpty(user))
        {
            await client.AuthenticateAsync(user, pass);
        }

        await client.SendAsync(message);
        await client.DisconnectAsync(true);

        // log
        var userEntity = _context.Users.FirstOrDefault(u => u.Email == to);
        var userId = userEntity?.Id ?? 0;
        _context.EmailLogs.Add(new EmailLog { UserId = userId, Subject = subject, SentAt = DateTime.UtcNow });
        await _context.SaveChangesAsync();
    }
}
