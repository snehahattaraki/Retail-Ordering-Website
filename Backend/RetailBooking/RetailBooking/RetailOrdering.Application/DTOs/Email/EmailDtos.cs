using System;

namespace RetailOrdering.Application.DTOs.Email
{
    public record EmailMessageDto(string To, string Subject, string Body);
}
