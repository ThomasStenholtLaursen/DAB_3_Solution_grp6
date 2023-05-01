using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB_3_Solution_grp6.MongoDb.DataAccess.Models
{
    public class Meal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; }

        [BsonElement("mealName")]
        public string MealName { get; }

        [BsonElement("canteenId")]
        public string CanteenName { get; }

        [BsonElement("reservationId")]
        public ObjectId? ReservationId { get; }

        public Meal(string mealName, string canteenName, ObjectId? reservationId)
        {
            Id = ObjectId.GenerateNewId();
            MealName = mealName;
            CanteenName = canteenName;
            ReservationId = reservationId;
        }
    }

}
