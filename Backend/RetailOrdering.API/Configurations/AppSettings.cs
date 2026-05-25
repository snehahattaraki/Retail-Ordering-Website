namespace RetailOrdering.API.Configurations
{
    public class AppSettings
    {
        public JwtSettings Jwt { get; set; } = new();
        public EmailSettings Email { get; set; } = new();
        public FileStorageSettings FileStorage { get; set; } = new();
        public string AllowedHosts { get; set; } = "*";
    }
}
