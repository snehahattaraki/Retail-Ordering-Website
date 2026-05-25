using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailOrdering.Domain.Entities;

namespace RetailOrdering.Infrastructure.Configurations
{
    public class PackagingConfiguration : IEntityTypeConfiguration<Packaging>
    {
        public void Configure(EntityTypeBuilder<Packaging> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
