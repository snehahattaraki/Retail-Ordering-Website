using System.ComponentModel.DataAnnotations;

namespace RetailOrdering.Application.DTOs.Payment;

public class PaymentDto
{
    [Required]
    public decimal Amount { get; set; }

    public string Method { get; set; } = "COD";
}