using Microsoft.EntityFrameworkCore;

namespace DAB_3_Solution_grp6.MSSQL.DataAccess.Repositories.Global
{
    public class GlobalRepository : IGlobalRepository
    {
        private readonly CanteenAppDbContext _dbContext;

        public GlobalRepository(CanteenAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task RemoveAll()
        {
            var meals = await _dbContext.Meals.ToListAsync();
            _dbContext.Meals.RemoveRange(meals);

            var reservations = await _dbContext.Reservations.ToListAsync();
            _dbContext.Reservations.RemoveRange(reservations);

            var ratings = await _dbContext.Ratings.ToListAsync();
            _dbContext.Ratings.RemoveRange(ratings);

            var customers = await _dbContext.Customers.ToListAsync();
            _dbContext.Customers.RemoveRange(customers);

            var menus= await _dbContext.Menus.ToListAsync();
            _dbContext.Menus.RemoveRange(menus);

            var staff = await _dbContext.Staff.ToListAsync();
            _dbContext.Staff.RemoveRange(staff);

            var canteens = await _dbContext.Canteens.ToListAsync();
            _dbContext.Canteens.RemoveRange(canteens);

            await _dbContext.SaveChangesAsync();
        }
    }
}
