using DAB_3_Solution_grp6.MSSQL.DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DAB_3_Solution_grp6.MSSQL.DataAccess.Repositories.Canteen
{
    public class CanteenRepository : ICanteenRepository
    {
        private readonly CanteenAppDbContext _dbContext;

        public CanteenRepository(CanteenAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Entities.Canteen>> GetAllCanteens()
        {
            var canteens = await _dbContext.Canteens.ToListAsync();

            return canteens;
        }

        public async Task<Entities.Canteen> GetCanteenWithMenusByNameAsync(string canteenName)
        {
            var canteen = await _dbContext.Canteens
                .Include(x=> x.Menus)
                .FirstOrDefaultAsync(x => x.Name == canteenName);

            return canteen ?? throw new CanteenNotFoundException();
        }

        public async Task<Entities.Canteen> GetCanteenWithMenusAndReservationsByNameAsync(string canteenName)
        {
            var canteen = await _dbContext.Canteens
                .Include(x => x.Menus)!
                .ThenInclude(y => y.Reservations)
                .FirstOrDefaultAsync(x => x.Name == canteenName);

            return canteen ?? throw new CanteenNotFoundException();
        }

        public async Task<Entities.Canteen> GetCanteenWithMealsByNameAsync(string canteenName)
        {
            var canteen = await _dbContext.Canteens
                .Include(x => x.Meals)
                .FirstOrDefaultAsync(x => x.Name == canteenName);

            return canteen ?? throw new CanteenNotFoundException();
        }

        public async Task<IReadOnlyList<Entities.Canteen>> GetNearbyCanteenMealsByNameAsync(string canteenName)
        {
            var canteen = await _dbContext.Canteens.FirstOrDefaultAsync(x => x.Name == canteenName) ?? throw new CanteenNotFoundException();

            var nearbyCanteens = await _dbContext.Canteens
                .Include(x => x.Meals)
                .Where(x => x.PostalCode == canteen.PostalCode)
                .ToListAsync();

            nearbyCanteens.Remove(canteen!);

            return nearbyCanteens;
        }

        public async Task<IReadOnlyList<Entities.Canteen>> GetAllCanteensWithRatingsAsync()
        {
            var canteens = await _dbContext.Canteens
                .Include(x => x.Ratings)
                .ToListAsync();

            return canteens;
        }

        public async Task<Entities.Canteen> GetAllCanteenWithStaffByNameAsync(string canteenName)
        {
            var canteen = await _dbContext.Canteens
                .Include(x => x.Staff)
                .FirstOrDefaultAsync(x => x.Name == canteenName);

            return canteen ?? throw new CanteenNotFoundException();
        }
    }
}
