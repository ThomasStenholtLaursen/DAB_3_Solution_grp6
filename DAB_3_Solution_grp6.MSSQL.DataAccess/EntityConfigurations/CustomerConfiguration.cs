using DAB_3_Solution_grp6.MSSQL.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAB_3_Solution_grp6.MSSQL.DataAccess.EntityConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.AuId);

            builder.Property(x => x.AuId)
                .HasMaxLength(10);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(c => c.Ratings)
                .WithOne()
                .HasForeignKey(r => r.AuId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.Reservations)
                .WithOne()
                .HasForeignKey(r => r.AuId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
