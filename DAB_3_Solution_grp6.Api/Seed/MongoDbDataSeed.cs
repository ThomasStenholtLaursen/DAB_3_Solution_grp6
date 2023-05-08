using DAB_3_Solution_grp6.MongoDb.DataAccess.Models;
using DAB_3_Solution_grp6.MongoDb.DataAccess.Services;
using MongoDB.Bson;

namespace DAB_3_Solution_grp6.Api.Seed
{
    public class MongoDbDataSeed
    {
        private readonly MongoDbCanteenAppService _mongoDbCanteenAppService;

        public MongoDbDataSeed(MongoDbCanteenAppService mongoDbCanteenAppService)
        {
            _mongoDbCanteenAppService = mongoDbCanteenAppService;
        }

        public async void SeedDataMongoDb(MongoDbCanteenAppService mongoDbService)
        {
            var ratingCount = await _mongoDbCanteenAppService.GetRatingCountAsync();
            var canteenCount = await _mongoDbCanteenAppService.GetCanteenCountAsync();
            var menuCount = await _mongoDbCanteenAppService.GetMenuCountAsync();
            var reservationCount = await _mongoDbCanteenAppService.GetReservationCountAsync();
            var customerCount = await _mongoDbCanteenAppService.GetCustomerCountAsync();
            var mealCount = await _mongoDbCanteenAppService.GetMealCountAsync();

            if (ratingCount != 0 || canteenCount != 0 || menuCount != 0 || reservationCount != 0 || customerCount != 0 || mealCount != 0) return;


            var auIds = GenerateAuIds(10);
            var canteenIds = GenerateIdentifiers(10);
            var menuIds = GenerateIdentifiers(10);
            var reservationIds = GenerateIdentifiers(20);

            await SeedCanteens(mongoDbService, canteenIds);
            await SeedCustomers(mongoDbService, auIds);
            await SeedRatings(mongoDbService, canteenIds, auIds);
            await SeedMenus(mongoDbService, menuIds);
            await SeedReservations(mongoDbService, menuIds, auIds, reservationIds);
            await SeedMeals(mongoDbService, reservationIds);
        }

        private static async Task SeedCanteens(MongoDbCanteenAppService mongodbService, IReadOnlyList<ObjectId> canteenIds)
        {
            var canteens = new List<Canteen>();

            var c0 = new Canteen("Kgl. Bibliotek", "Victor Albecks Vej 1", "8000", canteenIds[0])
            {
                Staff = new List<Staff>
                {
                    new("Kgl. Bibliotek","Jens B.", "Cook", 35000),
                    new("Kgl. Bibliotek", "Mette J.", "Cook", 35000),
                    new("Kgl. Bibliotek", "Henrik A.", "Waiter", 23500),
                    new("Kgl. Bibliotek",  "Gitte G.", "Waiter", 24250),
                    new("Kgl. Bibliotek", "Frederik F.", "Cleaner", 19000),
                }
            };

            var c1 = new Canteen("Mathematical Canteen", "Ny Munkegade 116", "8000", canteenIds[1])
            {
                Staff = new List<Staff>
                {
                    new("Mathematical Canteen", "Søren A.", "Cook", 35250),
                    new("Mathematical Canteen", "Claus I.", "Cook", 35300),
                    new("Mathematical Canteen", "Theis L.", "Waiter", 23500),
                    new("Mathematical Canteen", "Wilma F..", "Waiter", 24250),
                    new("Mathematical Canteen", "Kristian E.", "Cleaner", 18000),
                }
            };

            var c2 = new Canteen("INCUBA Katrinebjerg", "Åbogade 15", "8200", canteenIds[2])
            {
                Staff = new List<Staff>
                {
                    new("INCUBA Katrinebjerg", "Marianne J.", "Cook", 34500),
                    new("INCUBA Katrinebjerg", "Rasmus B.", "Waiter", 22500),
                }
            };

            var c3 = new Canteen("Chemical Canteen", "Langelandsgade 140", "8000", canteenIds[3])
            {
                Staff = new List<Staff>
                {
                    new("Chemical Canteen", "Emma K.", "Cleaner", 18750),
                    new("Chemical Canteen", "Lars P.", "Cook", 36000),
                    new("Chemical Canteen", "Maria G.", "Waiter", 23000),
                }
            };

            var c4 = new Canteen("MoCa MaD", "Moesgård Allé 20", "8270", canteenIds[4])
            {
                Staff = new List<Staff>
                {
                    new("MoCa MaD", "Anders S.", "Cook", 35500),
                    new("MoCa MaD", "Camilla M.", "Waiter", 24000),
                    new("MoCa MaD", "Nikolaj H.", "Cleaner", 18500)
                }
            };

            canteens.Add(c0);
            canteens.Add(c1);
            canteens.Add(c2);
            canteens.Add(c3);
            canteens.Add(c4);

            await mongodbService.InsertManyCanteensAsync(canteens);
        }

        private static async Task SeedCustomers(MongoDbCanteenAppService mongodbService, IReadOnlyList<string> auIds)
        {
            var customers = new List<Customer>
            {
                new(auIds[0], "Jens", "Henriksen"),
                new(auIds[1], "Gitte", "Frederiksen"),
                new(auIds[2], "Claus", "Nielsen"),
                new(auIds[3], "Hanne", "Sørensen"),
                new(auIds[4], "Hans", "Larsen")
            };

            await mongodbService.InsertManyCustomerAsync(customers);
        }

        private static async Task SeedRatings(MongoDbCanteenAppService mongodbService, IReadOnlyList<ObjectId> canteenIds, IReadOnlyList<string> auIds)
        {
            var ratings = new List<Rating>
            {
                new("Kgl. Bibliotek", (decimal)2.5, DateTime.Now, "Not to my liking", auIds[0]),
                new("Kgl. Bibliotek", (decimal)4.5, DateTime.Now, "Much better now!", auIds[0]),
                new("Kgl. Bibliotek",(decimal)3.5, DateTime.Now, "it was OK", auIds[1]),
                new("Mathematical Canteen", (decimal)2.5, DateTime.Now, "Very bland!", auIds[2]),
                new("Mathematical Canteen", (decimal)3.5, DateTime.Now, "Too expensive", auIds[3]),
                new("Mathematical Canteen",  (decimal)1.5, DateTime.Now, "Too expensive", auIds[1]),
                new("INCUBA Katrinebjerg",5, DateTime.Now, "Prices are a bit high", auIds[2]),
                new("INCUBA Katrinebjerg", (decimal)2.5, DateTime.Now, null, auIds[4]),
                new("INCUBA Katrinebjerg", (decimal)3.5, DateTime.Now, null, auIds[4]),
                new("Chemical Canteen", (decimal)4.5, DateTime.Now, "Good meal", auIds[3]),
                new("Chemical Canteen", (decimal)0.5, DateTime.Now, "I almost puked!", auIds[4]),
                new("Chemical Canteen",1, DateTime.Now, "Seemed to be rotten", auIds[3]),
                new("MoCa MaD",5, DateTime.Now, "Very nice!", auIds[4]),
                new("MoCa MaD",(decimal)3.5, DateTime.Now, "I liked it", auIds[1]),
                new("MoCa MaD",4, DateTime.Now, "Good!", auIds[2]),
            };

            await mongodbService.InsertManyRatingsAsync(ratings);
        }

        private static async Task SeedMenus(MongoDbCanteenAppService mongodbService, IReadOnlyList<ObjectId> menuIds)
        {
            var menus = new List<Menu>
            {
                new("Soup", "Pizza", DateTime.Now, "Kgl. Bibliotek", menuIds[0]),
                new("Lasagne", "Hot Dog", DateTime.Now,"Mathematical Canteen", menuIds[1]),
                new("Meatballs", "Taco", DateTime.Now, "INCUBA Katrinebjerg", menuIds[2]),
                new("Wok", "Kebab", DateTime.Now,"Chemical Canteen", menuIds[3]),
                new("Cod with peas", "Burger", DateTime.Now, "MoCa MaD", menuIds[4]),
            };

            await mongodbService.InsertManyMenusAsync(menus);
        }

        private static async Task SeedReservations(MongoDbCanteenAppService mongodbService, IReadOnlyList<ObjectId> menuIds, IReadOnlyList<string> auIds, IReadOnlyList<ObjectId> reservationIds)
        {
            var reservations = new List<Reservation>
            {
                new(1, 2, DateTime.Now, auIds[0], menuIds[0], reservationIds[0]),
                new(0, 2, DateTime.Now, auIds[1], menuIds[0], reservationIds[1]),
                new(1, 1, DateTime.Now, auIds[3], menuIds[0], reservationIds[2]),
                new(2, 2, DateTime.Now, auIds[2], menuIds[0], reservationIds[3]),
                new(4, 2, DateTime.Now, auIds[2], menuIds[1], reservationIds[4]),
                new(0, 2, DateTime.Now, auIds[4], menuIds[1], reservationIds[5]),
                new(1, 0, DateTime.Now, auIds[3], menuIds[2], reservationIds[6]),
                new(0, 1, DateTime.Now, auIds[0], menuIds[2], reservationIds[7]),
                new(1, 1, DateTime.Now, auIds[1], menuIds[3], reservationIds[8]),
                new(5, 1, DateTime.Now, auIds[2], menuIds[3], reservationIds[9]),
                new(3, 2, DateTime.Now, auIds[4], menuIds[4], reservationIds[10]),
                new(1, 2, DateTime.Now, auIds[3], menuIds[4], reservationIds[11]),
            };

            await mongodbService.InsertManyReservationAsync(reservations);
        }


        private static async Task SeedMeals(MongoDbCanteenAppService mongodbService, IReadOnlyList<ObjectId> reservationIds)
        {
            var meals = new List<Meal>
            {
                new("Soup", "Kgl. Bibliotek", reservationIds[0]),
                new("Soup", "Kgl. Bibliotek", reservationIds[0]),
                new("Pizza", "Kgl. Bibliotek", reservationIds[0]),
                new("Pizza", "Kgl. Bibliotek", reservationIds[0]),
                new("Pizza", "Kgl. Bibliotek", reservationIds[1]),
                new("Pizza", "Kgl. Bibliotek", reservationIds[1]),
                new("Pizza", "Kgl. Bibliotek", reservationIds[2]),
                new("Soup", "Kgl. Bibliotek", reservationIds[2]),
                new("Soup", "Kgl. Bibliotek", reservationIds[3]),
                new("Soup", "Kgl. Bibliotek", reservationIds[3]),
                new("Pizza", "Kgl. Bibliotek", reservationIds[3]),
                new("Pizza", "Kgl. Bibliotek", reservationIds[3]),
                new("Pizza", "Kgl. Bibliotek", null),
                new("Pizza", "Kgl. Bibliotek", null),
                new("Pizza", "Kgl. Bibliotek", null),
                new("Pizza", "Kgl. Bibliotek", null),
                new("Lasagne", "Mathematical Canteen", reservationIds[4]),
                new("Lasagne", "Mathematical Canteen", reservationIds[4]),
                new("Lasagne", "Mathematical Canteen", reservationIds[4]),
                new("Lasagne", "Mathematical Canteen", reservationIds[4]),
                new("Hot Dog", "Mathematical Canteen", reservationIds[4]),
                new("Hot Dog", "Mathematical Canteen", reservationIds[4]),
                new("Hot Dog", "Mathematical Canteen", reservationIds[5]),
                new("Hot Dog", "Mathematical Canteen", reservationIds[5]),
                new("Hot Dog", "Mathematical Canteen", null),
                new("Hot Dog", "Mathematical Canteen", null),
                new("Hot Dog", "Mathematical Canteen", null),
                new("Meatballs", "INCUBA Katrinebjerg", reservationIds[6]),
                new("Taco", "INCUBA Katrinebjerg", reservationIds[7]),
                new("Meatballs", "INCUBA Katrinebjerg", null),
                new("Meatballs", "INCUBA Katrinebjerg", null),
                new("Meatballs", "INCUBA Katrinebjerg", null),
                new("Meatballs", "INCUBA Katrinebjerg", null),
                new("Meatballs", "INCUBA Katrinebjerg", null),
                new("Taco", "INCUBA Katrinebjerg", null),
                new("Taco", "INCUBA Katrinebjerg", null),
                new("Taco", "INCUBA Katrinebjerg", null),
                new("Taco", "INCUBA Katrinebjerg", null),
                new("Taco", "INCUBA Katrinebjerg", null),
                new("Taco", "INCUBA Katrinebjerg", null),
                new("Wok", "Chemical Canteen", reservationIds[8]),
                new("Kebab", "Chemical Canteen", reservationIds[8]),
                new("Kebab", "Chemical Canteen", reservationIds[9]),
                new("Wok", "Chemical Canteen", reservationIds[9]),
                new("Wok", "Chemical Canteen", reservationIds[9]),
                new("Wok", "Chemical Canteen", reservationIds[9]),
                new("Wok", "Chemical Canteen", reservationIds[9]),
                new("Wok", "Chemical Canteen", reservationIds[9]),
                new("Wok", "Chemical Canteen", null),
                new("Wok", "Chemical Canteen", null),
                new("Wok", "Chemical Canteen", null),
                new("Wok", "Chemical Canteen", null),
                new("Kebab", "Chemical Canteen", null),
                new("Kebab", "Chemical Canteen", null),
                new("Kebab", "Chemical Canteen", null),
                new("Kebab", "Chemical Canteen", null),
                new("Cod with peas", "MoCa MaD", reservationIds[10]),
                new("Cod with peas", "MoCa MaD", reservationIds[10]),
                new("Cod with peas", "MoCa MaD", reservationIds[10]),
                new("Burger", "MoCa MaD", reservationIds[10]),
                new("Burger", "MoCa MaD", reservationIds[10]),
                new("Cod with peas", "MoCa MaD", reservationIds[11]),
                new("Burger", "MoCa MaD", reservationIds[11]),
                new("Burger", "MoCa MaD", reservationIds[11]),
                new("Cod with peas", "MoCa MaD", null),
                new("Cod with peas", "MoCa MaD", null),
                new("Cod with peas", "MoCa MaD", null),
                new("Cod with peas", "MoCa MaD", null),
                new("Cod with peas", "MoCa MaD", null),
                new("Burger", "MoCa MaD", null),
                new("Burger", "MoCa MaD", null),
                new("Burger", "MoCa MaD", null),
            };

            await mongodbService.InsertManyMealAsync(meals);
        }

        private static List<ObjectId> GenerateIdentifiers(int count)
        {
            var ids = new List<ObjectId>();

            for (var i = 0; i < count; i++)
            {
                ids.Add(ObjectId.GenerateNewId());
            }

            return ids;
        }

        private static List<string> GenerateAuIds(int count)
        {
            var ids = new List<string>();

            for (var i = 0; i < count; i++)
            {
                var randomNumber = new Random().Next(100000, 999999);

                var id = "AU" + randomNumber;

                ids.Add(id);
            }

            return ids;
        }
    }
}


