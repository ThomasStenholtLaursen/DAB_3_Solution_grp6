using DAB_3_Solution_grp6.MSSQL.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAB_3_Solution_grp6.MSSQL.DataAccess
{
#pragma warning disable CS1591
    public class CanteenAppDbContext : DbContext
    {
        public DbSet<Canteen> Canteens { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<Meal> Meals { get; set; } = null!;
        public DbSet<Menu> Menus { get; set; } = null!;
        public DbSet<Staff> Staff { get; set; } = null!;

        public CanteenAppDbContext(DbContextOptions<CanteenAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CanteenAppDbContext).Assembly);
        }
    }
#pragma warning restore CS1591
}
