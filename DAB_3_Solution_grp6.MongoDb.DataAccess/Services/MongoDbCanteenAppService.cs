using DAB_3_Solution_grp6.MongoDb.DataAccess.Models;
using DAB_3_Solution_grp6.MongoDb.DataAccess.MongoDbSettingsAccess;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DAB_3_Solution_grp6.MongoDb.DataAccess.Services;

public class MongoDbCanteenAppService
{
    private readonly IMongoCollection<Canteen> _canteenCollection;
    private readonly IMongoCollection<Customer> _customerCollection;
    private readonly IMongoCollection<Rating> _ratingCollection;
    private readonly IMongoCollection<Menu> _menuCollection;
    private readonly IMongoCollection<Meal> _mealCollection;
    private readonly IMongoCollection<Reservation> _reservationCollection;

    public MongoDbCanteenAppService(
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
    public async Task CreateAsync(Canteen newBook) =>
        await _canteenCollection.InsertOneAsync(newBook);

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


    public async Task<List<Staff>> GetCanteenStaff(string canteenName) => (await _canteenCollection.Find(x => x.Name == canteenName).FirstOrDefaultAsync()).Staff;

    public async Task<Menu> GetCanteenMenu(string canteenName) => await _menuCollection.Find(x => x.CanteenName == canteenName).FirstOrDefaultAsync();

    public async Task<Reservation> GetReservationsByAuId(string auId) => await _reservationCollection.Find(x => x.AuId == auId ).FirstOrDefaultAsync();

    public async Task<List<Meal>> GetMealsByAuId(string auId)
    {
        var reservationIDs = await _reservationCollection.Find(x => x.AuId == auId)
            .Project(x => x.Id).ToListAsync();

        var meals = await _mealCollection.Find(x => reservationIDs.Contains(x.Id))
            .ToListAsync();

        return meals;
    }

    public async Task<List<Reservation>> GetReservationsForCanteen(string canteenName)
    {
        var canteenMenu = await GetCanteenMenu(canteenName);
        var reservationsForCanteen = await _reservationCollection.Find(x => x.MenuId == canteenMenu.Id).ToListAsync();

        return reservationsForCanteen;
    }

    public async Task<Menu> GetMenuForCanteen(string canteenName)
    {
        var canteenMenu = await GetCanteenMenu(canteenName);

        return canteenMenu;
    }
}