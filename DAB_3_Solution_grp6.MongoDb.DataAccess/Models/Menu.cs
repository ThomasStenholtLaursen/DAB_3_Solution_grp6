using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DAB_3_Solution_grp6.MongoDb.DataAccess.Models
{
    public class Menu
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; }

        [BsonElement("warmDishName")]
        public string WarmDishName { get; }

        [BsonElement("streetFoodName")]
        public string StreetFoodName { get; }

        [BsonElement("created")]
        public DateTime Created { get; }

        [BsonElement("canteenId")]
        public string CanteenName { get; }

        public Menu(string warmDishName, string streetFoodName, DateTime created, string canteenName)
        {
            Id = ObjectId.GenerateNewId();
            WarmDishName = warmDishName;
            StreetFoodName = streetFoodName;
            Created = created;
            CanteenName = canteenName;
        }
    }
}
