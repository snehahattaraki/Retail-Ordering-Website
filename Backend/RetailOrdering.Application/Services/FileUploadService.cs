using Microsoft.AspNetCore.Http;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class FileUploadService : IFileUploadService
    {
        public Task<string> UploadFileAsync(IFormFile file, string folder) => Task.FromResult(string.Empty);
    }
}
