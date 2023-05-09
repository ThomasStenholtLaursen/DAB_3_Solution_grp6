using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DAB_3_Solution_grp6.MongoDb.DataAccess.Models
{
    public class Rating
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; }

        [BsonElement("canteenId")]
        public string CanteenName { get; }

        [BsonElement("stars")]
        public decimal Stars { get; }

        [BsonElement("created")]
        public DateTime Created { get; }

        [BsonElement("comment")]
        public string? Comment { get; }

        [BsonElement("auId")]
        public string? AuId { get; }

        public Rating(string canteenName, decimal stars, DateTime created, string? comment, string? auId, ObjectId id = default)
        {
            Id = id == default ? ObjectId.GenerateNewId() : id;
            CanteenName = canteenName;
            Stars = stars;
            Created = created;
            Comment = comment;
            AuId = auId;
        }
    }
}

