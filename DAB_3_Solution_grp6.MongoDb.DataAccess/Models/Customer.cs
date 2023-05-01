using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DAB_3_Solution_grp6.MongoDb.DataAccess.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; }

        [BsonElement("auId")]
        public string AuId { get; }

        [BsonElement("firstName")]
        public string FirstName { get; }

        [BsonElement("lastName")]
        public string LastName { get; }

        public Customer(string auId, string firstName, string lastName)
        {
            Id = ObjectId.GenerateNewId();
            AuId = auId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
