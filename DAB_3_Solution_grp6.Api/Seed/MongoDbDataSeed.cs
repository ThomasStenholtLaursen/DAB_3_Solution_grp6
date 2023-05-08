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
            var menuCount = await _mongoDbCanteenAppService.GetMenuCountAsync();
            var reservationCount= await _mongoDbCanteenAppService.GetReservationCountAsync();
            var customerCount = await _mongoDbCanteenAppService.GetCustomerCountAsync();
            var mealCount = await _mongoDbCanteenAppService.GetMealCountAsync();


            if (ratingCount != 0 || canteenCount != 0 || menuCount != 0 || reservationCount!= 0 || customerCount != 0 || mealCount !=0) return;

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
                    new("Kgl. Bibliotek", "Jens B.", "Cook", 35000),
                    new("Kgl. Bibliotek", "Abdul", "Cook", 35000),
                }
            };
            canteens!.Add(c1);


            var menus = new List<Menu>
            {
                new ("Soup", "Pizza", DateTime.Now,"Kgl. Bibliotek"),
                new ("Lasagne", "Hot Dog", DateTime.Now, ""),
                new ("Meatballs", "Taco", DateTime.Now, ""),
                new ("Wok", "Kebab", DateTime.Now, ""),
                new ("Cod with peas", "Burger", DateTime.Now,""),
               
            };

            var customers = new List<Customer>
            {
                new Customer("au001", "John", "Doe"),
                new Customer("au002", "Jane", "Smith"),
                new Customer("au003", "Bob", "Johnson"),
                new Customer("au004", "Alice", "Williams"),
                new Customer("au005", "David", "Brown"),
            };


            var reservations = new List<Reservation>
            {
                 new Reservation(2, 1, DateTime.Now, "au001", menus[0].Id),
                 new Reservation(3, null, DateTime.Now, "au002", menus[1].Id),
                 new Reservation(null, 2, DateTime.Now, "au003", menus[2].Id),
                 new Reservation(1, 1, DateTime.Now, "au004", menus[3].Id),
                 new Reservation(null, null, DateTime.Now, "au005", menus[4].Id),
            };


            var meals = new List<Meal>
            {
                 new Meal("Pizza", "Kgl. Bibliotek", reservations[0].Id),  
                 new Meal("Hot Dog", "Kgl. Bibliotek", reservations[1].Id),  
                 new Meal("Taco", "Kgl. Bibliotek", reservations[2].Id),  
                 new Meal("Kebab", "Kgl. Bibliotek", reservations[3].Id),  
            };




            await _mongoDbCanteenAppService.InsertManyCanteensAsync(canteens);

            await _mongoDbCanteenAppService.InsertManyRatingsAsync(ratings);

            await _mongoDbCanteenAppService.InsertManyMenusAsync(menus);


            await _mongoDbCanteenAppService.InsertManyCustomerAsync(customers);

            await _mongoDbCanteenAppService.InsertManyReservationAsync(reservations);

            await _mongoDbCanteenAppService.InsertManyMealAsync(meals);

        }
    }
}


