using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarInfo.Entities.EntityConfiguration
{
    public class SuperCarConfiguration : IEntityTypeConfiguration<SuperCar>
    {
        void IEntityTypeConfiguration<SuperCar>.Configure(EntityTypeBuilder<SuperCar> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
