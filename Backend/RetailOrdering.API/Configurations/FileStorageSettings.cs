namespace RetailOrdering.API.Configurations
{
    public class FileStorageSettings
    {
        public string ProductsPath { get; set; } = string.Empty;
        public string InvoicesPath { get; set; } = string.Empty;
        public string ProfilesPath { get; set; } = string.Empty;
        public long MaxFileSizeBytes { get; set; }
    }
}
