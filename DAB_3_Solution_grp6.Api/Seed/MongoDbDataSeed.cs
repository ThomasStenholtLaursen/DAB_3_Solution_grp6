using DAB_3_Solution_grp6.MongoDb.DataAccess.Models;
using DAB_3_Solution_grp6.MongoDb.DataAccess.Services;

namespace DAB_3_Solution_grp6.Api.Seed
{
    public class MongoDbDataSeed
    {
        private readonly MongoDbCanteenAppService _mongoDbCanteenAppService;

        public MongoDbDataSeed(MongoDbCanteenAppService mongoDbCanteenAppService)
        {
            _mongoDbCanteenAppService = mongoDbCanteenAppService;
        }

        public async void SeedData()
        {
            var ratingCount = await _mongoDbCanteenAppService.GetRatingCountAsync();
            var canteenCount = await _mongoDbCanteenAppService.GetCanteenCountAsync();

            if (ratingCount != 0 || canteenCount != 0) return;

            var ratings = new List<Rating>
            {
                new("Kgl. Bibliotek", 4.5m, DateTime.UtcNow, "Great food!", "User1"),
                new("Kgl. Bibliotek", 3.8m, DateTime.UtcNow, "Good service.", "User2"),
            };


            var canteens = new List<Canteen>();

            var c1 = new Canteen("Kgl. Bibliotek", "Victor Albecks Vej 1", "8000")
            {
                Staff = new List<Staff>
                {
                    new("Kgl. Bibliotek", "Jens B.", "Cook", 35000)
                }
            };

            canteens!.Add(c1);

            await _mongoDbCanteenAppService.InsertManyCanteensAsync(canteens);

            await _mongoDbCanteenAppService.InsertManyRatingsAsync(ratings);
        }
    }
}


