using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarInfo.Entities.EntityConfiguration
{
    public class CarTypeConfiguration:IEntityTypeConfiguration<CarType>
    {
        void IEntityTypeConfiguration<CarType>.Configure(EntityTypeBuilder<CarType> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
