namespace RetailOrdering.Application.DTOs.Email
{
    public class EmailResponseDto
    {
        public bool IsSent { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
