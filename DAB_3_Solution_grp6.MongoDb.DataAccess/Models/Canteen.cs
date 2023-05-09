using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB_3_Solution_grp6.MongoDb.DataAccess.Models
{
    public class Canteen
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("postalCode")]
        public string PostalCode { get; set; }

        [BsonElement("staff")]
        public List<Staff> Staff { get; set; } = new();

        public Canteen(string name, string address, string postalCode, ObjectId id = default)
        {
            Id = id == default ? ObjectId.GenerateNewId() : id;
            Name = name;
            Address = address;
            PostalCode = postalCode;
        }
    }
}
