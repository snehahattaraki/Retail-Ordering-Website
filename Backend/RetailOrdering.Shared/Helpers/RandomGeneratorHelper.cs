using System.Text;

namespace RetailOrdering.Shared.Helpers
{
    public static class RandomGeneratorHelper
    {
        public static string GenerateNumericCode(int length = 6)
        {
            var random = new Random();
            var builder = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                builder.Append(random.Next(0, 10));
            }
            return builder.ToString();
        }
    }
}
