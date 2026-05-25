namespace RetailOrdering.Application.DTOs.Common
{
    public class ErrorResponseDto
    {
        public string Error { get; set; } = string.Empty;
        public List<string>? Details { get; set; }
    }
}
