namespace DAB_3_Solution_grp6.Api.Controllers.MongoDb.CanteenApp.Response.Query3
{
    public class ReservationsQuantityMongoDbResponse
    {
        public WarmDishMongoDb WarmDishMongoDb { get; set; } = null!;
        public StreetFoodMongoDb StreetFoodMongoDb { get; set; } = null!;
    }

    public class WarmDishMongoDb
    {
        public string Name { get; set; } = null!;
        public int? Amount { get; set; }
    }

    public class StreetFoodMongoDb
    {
        public string Name { get; set; } = null!;
        public int? Amount { get; set; }
    }
}
