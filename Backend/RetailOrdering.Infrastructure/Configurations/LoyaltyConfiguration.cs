using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailOrdering.Domain.Entities;

namespace RetailOrdering.Infrastructure.Configurations
{
    public class LoyaltyConfiguration : IEntityTypeConfiguration<LoyaltyPoint>
    {
        public void Configure(EntityTypeBuilder<LoyaltyPoint> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
