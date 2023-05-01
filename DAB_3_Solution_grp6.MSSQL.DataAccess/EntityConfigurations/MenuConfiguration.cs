using DAB_3_Solution_grp6.MSSQL.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAB_3_Solution_grp6.MSSQL.DataAccess.EntityConfigurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(x => x.MenuId);

            builder.Property(x => x.CanteenId)
                .IsRequired();

            builder.Property(x => x.Created)
                .IsRequired();

            builder.Property(x => x.WarmDishName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.StreetFoodName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(x => x.Reservations)
                .WithOne()
                .HasForeignKey(x => x.MenuId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
