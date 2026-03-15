using Course.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.Order.Persistence.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Street).IsRequired().HasMaxLength(100);
                builder.Property(x => x.Province).IsRequired().HasMaxLength(100);
                builder.Property(x => x.District).IsRequired().HasMaxLength(100);
                builder.Property(x => x.Line).IsRequired().HasMaxLength(100);
                builder.Property(x => x.ZipCode).IsRequired().HasMaxLength(20);

        }
    }
}
