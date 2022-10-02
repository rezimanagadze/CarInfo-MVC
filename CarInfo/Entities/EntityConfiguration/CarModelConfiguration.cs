using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarInfo.Entities.EntityConfiguration
{
    public class CarModelConfiguration:IEntityTypeConfiguration<CarModel>
    {
        void IEntityTypeConfiguration<CarModel>.Configure(EntityTypeBuilder<CarModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.CarTypes);
        }
    }
}
