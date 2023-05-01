using DAB_3_Solution_grp6.MSSQL.DataAccess;
using DAB_3_Solution_grp6.MSSQL.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAB_3_Solution_grp6.Api.Seed
{
    public static class MssqlDataSeed
    {
        public static async Task Seed(CanteenAppDbContext context)
        {
            var dataExists =
                await context.Canteens.AnyAsync() ||
                await context.Reservations.AnyAsync() ||
                await context.Ratings.AnyAsync() ||
                await context.Menus.AnyAsync() ||
                await context.Reservations.AnyAsync() ||
                await context.Meals.AnyAsync() ||
                await context.Staff.AnyAsync();

            if (dataExists) return;

            var auIds = GenerateAuIds(10);
            var canteenIds = GenerateIdentifiers(10);
            var menuIds = GenerateIdentifiers(10);
            var reservationIds = GenerateIdentifiers(20);

            await SeedCanteens(context, canteenIds);
            await SeedCustomers(context, auIds);
            await SeedRatings(context, canteenIds, auIds);
            await SeedMenus(context, canteenIds, menuIds);
            await SeedReservations(context, menuIds, auIds, reservationIds);
            await SeedMeals(context, canteenIds, reservationIds);
            await SeedStaff(context, canteenIds);
        }

        private static async Task SeedCanteens(CanteenAppDbContext context, IReadOnlyList<Guid> canteenIds)
        {
            var canteens = new[]
            {
                new Canteen(canteenIds[0], "Kgl. Bibliotek","Victor Albecks Vej 1", "8000"),
                new Canteen(canteenIds[1], "Mathematical Canteen","Ny Munkegade 116", "8000"),
                new Canteen(canteenIds[2], "INCUBA Katrinebjerg","Åbogade 15", "8200"),
                new Canteen(canteenIds[3], "Chemical Canteen","Langelandsgade 140", "8000"),
                new Canteen(canteenIds[4], "MoCa MaD","Moesgård Allé 20", "8270")
            };

            context.Canteens.AddRange(canteens);
            await context.SaveChangesAsync();
        }

        private static async Task SeedCustomers(CanteenAppDbContext context, IReadOnlyList<string> auIds)
        {
            var customers = new[]
            {
                new Customer(auIds[0], "Jens", "Henriksen"),
                new Customer(auIds[1], "Gitte", "Frederiksen"),
                new Customer(auIds[2], "Claus", "Nielsen"),
                new Customer(auIds[3], "Hanne", "Sørensen"),
                new Customer(auIds[4], "Hans", "Larsen")
            };

            context.Customers.AddRange(customers);
            await context.SaveChangesAsync();
        }

        private static async Task SeedRatings(CanteenAppDbContext context, IReadOnlyList<Guid> canteenIds, IReadOnlyList<string> cprs)
        {
            var ratings = new[]
            {
                new Rating(Guid.NewGuid(), (decimal)2.5, DateTime.Now, "Not to my liking", cprs[0], canteenIds[0]),
                new Rating(Guid.NewGuid(), (decimal)4.5, DateTime.Now, "Much better now!", cprs[0], canteenIds[0]),
                new Rating(Guid.NewGuid(), (decimal)3.5, DateTime.Now, "it was OK", cprs[1], canteenIds[0]),
                new Rating(Guid.NewGuid(), (decimal)2.5, DateTime.Now, "Very bland!", cprs[2], canteenIds[1]),
                new Rating(Guid.NewGuid(), (decimal)3.5, DateTime.Now, "Too expensive", cprs[3], canteenIds[1]),
                new Rating(Guid.NewGuid(), (decimal)1.5, DateTime.Now, "Too expensive", cprs[1], canteenIds[1]),
                new Rating(Guid.NewGuid(), (decimal)5, DateTime.Now, "Prices are a bit high", cprs[2], canteenIds[2]),
                new Rating(Guid.NewGuid(), (decimal)2.5, DateTime.Now, null, cprs[4], canteenIds[2]),
                new Rating(Guid.NewGuid(), (decimal)3.5, DateTime.Now, null, cprs[4], canteenIds[2]),
                new Rating(Guid.NewGuid(), (decimal)4.5, DateTime.Now, "Good meal", cprs[3], canteenIds[3]),
                new Rating(Guid.NewGuid(), (decimal)0.5, DateTime.Now, "I almost puked!", cprs[4], canteenIds[3]),
                new Rating(Guid.NewGuid(), (decimal)1, DateTime.Now, "Seemed to be rotten", cprs[3], canteenIds[3]),
                new Rating(Guid.NewGuid(), (decimal)5, DateTime.Now, "Very nice!", cprs[4], canteenIds[4]),
                new Rating(Guid.NewGuid(), (decimal)3.5, DateTime.Now, "I liked it", cprs[1], canteenIds[4]),
                new Rating(Guid.NewGuid(), (decimal)4, DateTime.Now, "Good!", cprs[2], canteenIds[4]),
            };

            context.Ratings.AddRange(ratings);
            await context.SaveChangesAsync();
        }

        private static async Task SeedMenus(CanteenAppDbContext context, IReadOnlyList<Guid> canteenIds, IReadOnlyList<Guid> menuIds)
        {
            var menus = new[]
            {
                new Menu(menuIds[0], "Soup", "Pizza", DateTime.Now, canteenIds[0]),
                new Menu(menuIds[1], "Lasagne", "Hot Dog", DateTime.Now, canteenIds[1]),
                new Menu(menuIds[2], "Meatballs", "Taco", DateTime.Now, canteenIds[2]),
                new Menu(menuIds[3], "Wok", "Kebab", DateTime.Now, canteenIds[3]),
                new Menu(menuIds[4], "Cod with peas", "Burger", DateTime.Now, canteenIds[4]),
            };

            context.Menus.AddRange(menus);
            await context.SaveChangesAsync();
        }

        private static async Task SeedReservations(CanteenAppDbContext context, IReadOnlyList<Guid> menuIds, IReadOnlyList<string> auIds, IReadOnlyList<Guid> reservationIds)
        {
            var reservations = new[]
            {
                new Reservation(reservationIds[0], 1, 2, DateTime.Now, auIds[0], menuIds[0]),
                new Reservation(reservationIds[1], 0, 2, DateTime.Now, auIds[1], menuIds[0]),
                new Reservation(reservationIds[2], 1, 1, DateTime.Now, auIds[3], menuIds[0]),
                new Reservation(reservationIds[3], 2, 2, DateTime.Now, auIds[2], menuIds[0]),
                new Reservation(reservationIds[4], 4, 2, DateTime.Now, auIds[2], menuIds[1]), 
                new Reservation(reservationIds[5], 0, 2, DateTime.Now, auIds[4], menuIds[1]),
                new Reservation(reservationIds[6], 1, 0, DateTime.Now, auIds[3], menuIds[2]), 
                new Reservation(reservationIds[7], 0, 1, DateTime.Now, auIds[0], menuIds[2]),
                new Reservation(reservationIds[8], 1, 1, DateTime.Now, auIds[1], menuIds[3]), 
                new Reservation(reservationIds[9], 5, 1, DateTime.Now, auIds[2], menuIds[3]),
                new Reservation(reservationIds[10], 3, 2, DateTime.Now, auIds[4], menuIds[4]),
                new Reservation(reservationIds[11], 1, 2, DateTime.Now, auIds[3], menuIds[4]),
            };

            context.Reservations.AddRange(reservations);
            await context.SaveChangesAsync();
        }

        private static async Task SeedMeals(CanteenAppDbContext context, IReadOnlyList<Guid> canteenIds, IReadOnlyList<Guid> reservationIds)
        {
            var meals = new[]
            {
                new Meal(Guid.NewGuid(), "Soup", canteenIds[0], reservationIds[0]),
                new Meal(Guid.NewGuid(), "Pizza", canteenIds[0], reservationIds[0]),
                new Meal(Guid.NewGuid(), "Pizza", canteenIds[0], reservationIds[0]),
                new Meal(Guid.NewGuid(), "Pizza", canteenIds[0], reservationIds[1]),
                new Meal(Guid.NewGuid(), "Pizza", canteenIds[0], reservationIds[1]),
                new Meal(Guid.NewGuid(), "Pizza", canteenIds[0], reservationIds[2]),
                new Meal(Guid.NewGuid(), "Soup", canteenIds[0], reservationIds[2]),
                new Meal(Guid.NewGuid(), "Soup", canteenIds[0], reservationIds[3]),
                new Meal(Guid.NewGuid(), "Soup", canteenIds[0], reservationIds[3]),
                new Meal(Guid.NewGuid(), "Pizza", canteenIds[0], reservationIds[3]),
                new Meal(Guid.NewGuid(), "Pizza", canteenIds[0], reservationIds[3]),
                new Meal(Guid.NewGuid(), "Pizza", canteenIds[0], null),
                new Meal(Guid.NewGuid(), "Pizza", canteenIds[0], null),
                new Meal(Guid.NewGuid(), "Pizza", canteenIds[0], null),
                new Meal(Guid.NewGuid(), "Pizza", canteenIds[0], null),
                new Meal(Guid.NewGuid(), "Lasagne", canteenIds[1], reservationIds[4]),
                new Meal(Guid.NewGuid(), "Lasagne", canteenIds[1], reservationIds[4]),
                new Meal(Guid.NewGuid(), "Lasagne", canteenIds[1], reservationIds[4]),
                new Meal(Guid.NewGuid(), "Lasagne", canteenIds[1], reservationIds[4]),
                new Meal(Guid.NewGuid(), "Hot Dog", canteenIds[1], reservationIds[4]),
                new Meal(Guid.NewGuid(), "Hot Dog", canteenIds[1], reservationIds[4]),
                new Meal(Guid.NewGuid(), "Hot Dog", canteenIds[1], reservationIds[5]),
                new Meal(Guid.NewGuid(), "Hot Dog", canteenIds[1], reservationIds[5]),
                new Meal(Guid.NewGuid(), "Hot Dog", canteenIds[1], null),
                new Meal(Guid.NewGuid(), "Hot Dog", canteenIds[1], null),
                new Meal(Guid.NewGuid(), "Hot Dog", canteenIds[1], null),
                new Meal(Guid.NewGuid(), "Meatballs", canteenIds[2], reservationIds[6]),
                new Meal(Guid.NewGuid(), "Taco", canteenIds[2], reservationIds[7]),
                new Meal(Guid.NewGuid(), "Meatballs", canteenIds[2], null),
                new Meal(Guid.NewGuid(), "Meatballs", canteenIds[2], null),
                new Meal(Guid.NewGuid(), "Meatballs", canteenIds[2], null),
                new Meal(Guid.NewGuid(), "Meatballs", canteenIds[2], null),
                new Meal(Guid.NewGuid(), "Meatballs", canteenIds[2], null),
                new Meal(Guid.NewGuid(), "Taco", canteenIds[2], null),
                new Meal(Guid.NewGuid(), "Taco", canteenIds[2], null),
                new Meal(Guid.NewGuid(), "Taco", canteenIds[2], null),
                new Meal(Guid.NewGuid(), "Taco", canteenIds[2], null),
                new Meal(Guid.NewGuid(), "Taco", canteenIds[2], null),
                new Meal(Guid.NewGuid(), "Taco", canteenIds[2], null),
                new Meal(Guid.NewGuid(), "Wok", canteenIds[3], reservationIds[8]),
                new Meal(Guid.NewGuid(), "Kebab", canteenIds[3], reservationIds[8]),
                new Meal(Guid.NewGuid(), "Kebab", canteenIds[3], reservationIds[9]),
                new Meal(Guid.NewGuid(), "Wok", canteenIds[3], reservationIds[9]),
                new Meal(Guid.NewGuid(), "Wok", canteenIds[3], reservationIds[9]),
                new Meal(Guid.NewGuid(), "Wok", canteenIds[3], reservationIds[9]),
                new Meal(Guid.NewGuid(), "Wok", canteenIds[3], reservationIds[9]),
                new Meal(Guid.NewGuid(), "Wok", canteenIds[3], reservationIds[9]),
                new Meal(Guid.NewGuid(), "Wok", canteenIds[3], null),
                new Meal(Guid.NewGuid(), "Wok", canteenIds[3], null),
                new Meal(Guid.NewGuid(), "Wok", canteenIds[3], null),
                new Meal(Guid.NewGuid(), "Wok", canteenIds[3], null),
                new Meal(Guid.NewGuid(), "Kebab", canteenIds[3], null),
                new Meal(Guid.NewGuid(), "Kebab", canteenIds[3], null),
                new Meal(Guid.NewGuid(), "Kebab", canteenIds[3], null),
                new Meal(Guid.NewGuid(), "Kebab", canteenIds[3], null),
                new Meal(Guid.NewGuid(), "Cod with peas", canteenIds[4], reservationIds[10]),
                new Meal(Guid.NewGuid(), "Cod with peas", canteenIds[4], reservationIds[10]),
                new Meal(Guid.NewGuid(), "Cod with peas", canteenIds[4], reservationIds[10]),
                new Meal(Guid.NewGuid(), "Burger", canteenIds[4], reservationIds[10]),
                new Meal(Guid.NewGuid(), "Burger", canteenIds[4], reservationIds[10]),
                new Meal(Guid.NewGuid(), "Cod with peas", canteenIds[4], reservationIds[11]),
                new Meal(Guid.NewGuid(), "Burger", canteenIds[4], reservationIds[11]),
                new Meal(Guid.NewGuid(), "Burger", canteenIds[4], reservationIds[11]),
                new Meal(Guid.NewGuid(), "Cod with peas", canteenIds[4], null),
                new Meal(Guid.NewGuid(), "Cod with peas", canteenIds[4], null),
                new Meal(Guid.NewGuid(), "Cod with peas", canteenIds[4], null),
                new Meal(Guid.NewGuid(), "Cod with peas", canteenIds[4], null),
                new Meal(Guid.NewGuid(), "Cod with peas", canteenIds[4], null),
                new Meal(Guid.NewGuid(), "Burger", canteenIds[4], null),
                new Meal(Guid.NewGuid(), "Burger", canteenIds[4], null),
                new Meal(Guid.NewGuid(), "Burger", canteenIds[4], null),
            };

            context.Meals.AddRange(meals);
            await context.SaveChangesAsync();
        }

        private static async Task SeedStaff(CanteenAppDbContext context, IReadOnlyList<Guid> canteenIds)
        {
            var staff = new[]
            {
                new Staff(Guid.NewGuid(), canteenIds[0], "Jens B.", "Cook", 35000),
                new Staff(Guid.NewGuid(), canteenIds[0], "Mette J.", "Cook", 35000),
                new Staff(Guid.NewGuid(), canteenIds[0], "Henrik A.", "Waiter", 23500),
                new Staff(Guid.NewGuid(), canteenIds[0], "Gitte G.", "Waiter", 24250),
                new Staff(Guid.NewGuid(), canteenIds[0], "Frederik F.", "Cleaner", 19000),
                new Staff(Guid.NewGuid(), canteenIds[1], "Søren A.", "Cook", 35250),
                new Staff(Guid.NewGuid(), canteenIds[1], "Claus I.", "Cook", 35300),
                new Staff(Guid.NewGuid(), canteenIds[1], "Theis L.", "Waiter", 23500),
                new Staff(Guid.NewGuid(), canteenIds[1], "Wilma F..", "Waiter", 24250),
                new Staff(Guid.NewGuid(), canteenIds[1], "Kristian E.", "Cleaner", 18000),
                new Staff(Guid.NewGuid(), canteenIds[2], "Marianne J.", "Cook", 34500),
                new Staff(Guid.NewGuid(), canteenIds[2], "Rasmus B.", "Waiter", 22500),
                new Staff(Guid.NewGuid(), canteenIds[3], "Emma K.", "Cleaner", 18750),
                new Staff(Guid.NewGuid(), canteenIds[3], "Lars P.", "Cook", 36000),
                new Staff(Guid.NewGuid(), canteenIds[3], "Maria G.", "Waiter", 23000),
                new Staff(Guid.NewGuid(), canteenIds[4], "Anders S.", "Cook", 35500),
                new Staff(Guid.NewGuid(), canteenIds[4], "Camilla M.", "Waiter", 24000),
                new Staff(Guid.NewGuid(), canteenIds[4], "Nikolaj H.", "Cleaner", 18500)
            };

            context.Staff.AddRange(staff);
            await context.SaveChangesAsync();
        }

        private static List<Guid> GenerateIdentifiers(int count)
        {
            var ids = new List<Guid>();

            for (var i = 0; i < count; i++)
            {
                ids.Add(Guid.NewGuid());
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