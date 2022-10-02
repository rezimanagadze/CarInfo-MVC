using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarInfo.Entities.EntityConfiguration
{
    public class CatalogConfiguration:IEntityTypeConfiguration<Catalog>
    {
        public void Configure(EntityTypeBuilder<Catalog> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
