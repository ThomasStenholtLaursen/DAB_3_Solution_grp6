using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB_3_Solution_grp6.MongoDb.DataAccess.Models
{
    public class Staff
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; }

        [BsonElement("canteenId")]
        public string CanteenName { get; }

        [BsonElement("name")]
        public string Name { get; }

        [BsonElement("title")]
        public string Title { get; }

        [BsonElement("salary")]
        public int Salary { get; }

        public Staff(string canteenName, string name, string title, int salary, ObjectId id = default)
        {
            Id = id == default ? ObjectId.GenerateNewId() : id;
            CanteenName = canteenName;
            Name = name;
            Title = title;
            Salary = salary;
        }
    }
}
