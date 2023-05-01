﻿using DAB_3_Solution_grp6.MongoDb.DataAccess.Models;
using DAB_3_Solution_grp6.MongoDb.DataAccess.MongoDbSettings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DAB_3_Solution_grp6.MongoDb.DataAccess.Services;

public class CanteenService
{
    private readonly IMongoCollection<Canteen> _canteenCollection;
    private readonly IMongoCollection<Customer> _customerCollection;
    private readonly IMongoCollection<Rating> _ratingCollection;
    private readonly IMongoCollection<Menu> _menuCollection;
    private readonly IMongoCollection<Meal> _mealCollection;
    private readonly IMongoCollection<Reservation> _reservationCollection;

    public CanteenService(
        IOptions<Settings> databaseSettings)
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

    public async Task InsertManyCanteensAsync(IEnumerable<Canteen> canteens) =>
        await _canteenCollection.InsertManyAsync(canteens);

    public async Task InsertManyRatingsAsync(IEnumerable<Rating> ratings) =>
        await _ratingCollection.InsertManyAsync(ratings);

    //public async Task<Canteen> GetCanteenStaff(string canteenName)
    //{
    //    var canteens = await _canteenCollection.FindAsync(x => x.Name == canteenName);

    //    return canteens;
    //}

    public async Task<Canteen> GetCanteenStaff(string canteenName) => await _canteenCollection.Find(x => x.Name == canteenName).FirstOrDefaultAsync();
}