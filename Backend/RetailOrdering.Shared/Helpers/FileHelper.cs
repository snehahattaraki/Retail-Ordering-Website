namespace RetailOrdering.Shared.Helpers
{
    public static class FileHelper
    {
        public static string GetFileExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        public static string GetUniqueFileName(string fileName)
        {
            return $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        }
    }
}
