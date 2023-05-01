using DAB_3_Solution_grp6.MSSQL.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAB_3_Solution_grp6.MSSQL.DataAccess.EntityConfigurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(r => r.RatingId);

            builder.Property(r => r.Created)
                .IsRequired();

            builder.Property(x => x.CanteenId)
                .IsRequired();

            builder.Property(r => r.Stars)
                .HasPrecision(2, 1)
                .IsRequired();

            builder.Property(x => x.AuId)
                .IsRequired(false)
                .HasMaxLength(10);

            builder.Property(r => r.Comment)
                .IsRequired(false)
                .HasMaxLength(300);
        }
    }
}
