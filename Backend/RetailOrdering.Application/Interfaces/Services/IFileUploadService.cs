namespace RetailOrdering.Application.Interfaces.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file, string folder);
    }
}
