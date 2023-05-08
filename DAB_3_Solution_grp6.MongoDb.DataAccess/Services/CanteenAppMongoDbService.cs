using DAB_3_Solution_grp6.MongoDb.DataAccess.Models;
using DAB_3_Solution_grp6.MongoDb.DataAccess.MongoDbSettingsAccess;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DAB_3_Solution_grp6.MongoDb.DataAccess.Services;

public class CanteenAppMongoDbService
{
    private readonly IMongoCollection<Canteen> _canteenCollection;
    private readonly IMongoCollection<Customer> _customerCollection;
    private readonly IMongoCollection<Rating> _ratingCollection;
    private readonly IMongoCollection<Menu> _menuCollection;
    private readonly IMongoCollection<Meal> _mealCollection;
    private readonly IMongoCollection<Reservation> _reservationCollection;

    public CanteenAppMongoDbService(
        IOptions<MongoDbSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _canteenCollection = mongoDatabase.GetCollection<Canteen>(
            databaseSettings.Value.CanteenCollectionName);

        _customerCollection = mongoDatabase.GetCollection<Customer>(
            databaseSettings.Value.CustomerCollectionName);

        _ratingCollection = mongoDatabase.GetCollection<Rating>(
            databaseSettings.Value.RatingCollectionName);

        _menuCollection = mongoDatabase.GetCollection<Menu>(
            databaseSettings.Value.MenuCollectionName);

        _mealCollection = mongoDatabase.GetCollection<Meal>(
            databaseSettings.Value.MealCollectionName);

        _reservationCollection = mongoDatabase.GetCollection<Reservation>(
            databaseSettings.Value.ReservationCollectionName);
    }

    #region SeedMethods
    public async Task<long> GetCanteenCountAsync() =>
        await _canteenCollection.CountDocumentsAsync(FilterDefinition<Canteen>.Empty);

    public async Task<long> GetRatingCountAsync() =>
        await _ratingCollection.CountDocumentsAsync(FilterDefinition<Rating>.Empty);

    public async Task<long> GetMenuCountAsync() =>
      await _menuCollection.CountDocumentsAsync(FilterDefinition<Menu>.Empty);

    public async Task<long> GetCustomerCountAsync() =>
     await _customerCollection.CountDocumentsAsync(FilterDefinition<Customer>.Empty);

    public async Task<long> GetMealCountAsync() =>
    await _mealCollection.CountDocumentsAsync(FilterDefinition<Meal>.Empty);

    public async Task<long> GetReservationCountAsync() =>
    await _reservationCollection.CountDocumentsAsync(FilterDefinition<Reservation>.Empty);


    public async Task InsertManyCanteensAsync(IEnumerable<Canteen> canteens) =>
        await _canteenCollection.InsertManyAsync(canteens);

    public async Task InsertManyRatingsAsync(IEnumerable<Rating> ratings) =>
        await _ratingCollection.InsertManyAsync(ratings);

    public async Task InsertManyMenusAsync(IEnumerable<Menu> menus) =>
       await _menuCollection.InsertManyAsync(menus);

    public async Task InsertManyCustomerAsync(IEnumerable<Customer> customers) =>
   await _customerCollection.InsertManyAsync(customers);

    public async Task InsertManyReservationAsync(IEnumerable<Reservation> reservations) =>
    await _reservationCollection.InsertManyAsync(reservations);

    public async Task InsertManyMealAsync(IEnumerable<Meal> meals) =>
   await _mealCollection.InsertManyAsync(meals);
    #endregion

    public async Task<List<Staff>> GetCanteenStaff(string canteenName) => (await _canteenCollection.Find(x => x.Name == canteenName).FirstOrDefaultAsync()).Staff;

    public async Task<Menu> GetMenuForCanteen(string canteenName)
    {
        var today = DateTime.Today;
        var menu = await _menuCollection.Find(x => x.CanteenName == canteenName &&
                                                   x.Created.Year == today.Year &&
                                                   x.Created.Month == today.Month &&
                                                   x.Created.Day == today.Day).FirstOrDefaultAsync();

        return menu;
    }

    public async Task<List<Meal>> GetReservationsForAGivenCustomer(string auId)
    {
        var reservations = await _reservationCollection
            .Find(x => x.AuId == auId)
            .ToListAsync();

        var reservationIds = reservations.Select(r => r.Id).ToList();

        var meals = await _mealCollection
            .Find(x => reservationIds.Any(id => x.ReservationId == id))
            .ToListAsync();

        return meals;
    }

    public async Task<List<Meal>> GetCanceledMealsForCanteen(string canteenName)
    {
        var meals = await _mealCollection.Find(x => x.ReservationId == null && x.CanteenName == canteenName).ToListAsync();

        return meals;
    }

    public async Task<List<Meal>> GetCanceledMealsInNearbyCanteenForCanteen(string canteenName)
    {
        var canteen = await _canteenCollection.Find(x => x.Name == canteenName).FirstOrDefaultAsync();

        var nearbyCanteens = await _canteenCollection.Find(x => x.PostalCode == canteen.PostalCode && x.Name != canteen.Name).ToListAsync();

        var canceledMeals = await _mealCollection
            .Find(x => nearbyCanteens.Any(c => c.Name == x.CanteenName) && x.ReservationId == null)
            .ToListAsync();

        return canceledMeals;
    }

    public async Task<List<Rating>> GetAllRatings()
    {
        return await _ratingCollection.Find(x => true).ToListAsync();
    }

    public async Task<List<Reservation>> GetReservationsForCanteen(string canteenName)
    {
        var canteenMenu = await GetMenuForCanteen(canteenName);
        var reservationsForCanteen = await _reservationCollection.Find(x => x.MenuId == canteenMenu.Id).ToListAsync();

        return reservationsForCanteen;
    }
}