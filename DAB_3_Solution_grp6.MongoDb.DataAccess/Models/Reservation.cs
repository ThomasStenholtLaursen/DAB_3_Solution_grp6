using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DAB_3_Solution_grp6.MongoDb.DataAccess.Models
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; }

        [BsonElement("warmQuantity")]
        public int? WarmQuantity { get; }

        [BsonElement("streetQuantity")]
        public int? StreetQuantity { get; }

        [BsonElement("created")]
        public DateTime Created { get; }

        [BsonElement("auId")]
        public string AuId { get; }

        [BsonElement("menuId")]
        public ObjectId? MenuId { get; }

        public Reservation(int? warmQuantity, int? streetQuantity, DateTime created, string auId, ObjectId? menuId, ObjectId id = default)
        {
            Id = id == default ? ObjectId.GenerateNewId() : id;
            WarmQuantity = warmQuantity;
            StreetQuantity = streetQuantity;
            Created = created;
            AuId = auId;
            MenuId = menuId;
        }
    }
}
