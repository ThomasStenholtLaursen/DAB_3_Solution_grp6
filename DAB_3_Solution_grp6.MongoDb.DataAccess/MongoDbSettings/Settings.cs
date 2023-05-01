namespace DAB_3_Solution_grp6.MongoDb.DataAccess.MongoDbSettings
{
    public class Settings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CanteenCollectionName { get; set; } = null!;

        public string CustomerCollectionName { get; set; } = null!;

        public string MealCollectionName { get; set; } = null!;

        public string RatingCollectionName { get; set; } = null!;

        public string ReservationCollectionName { get; set; } = null!;

        public string MenuCollectionName { get; set; } = null!;
    }
}
