using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace server.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("brand")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Brand { get; set; } 

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("colors")]
        public List<string> Colors { get; set; }

        [BsonElement("specs")]
        public List<string> Specs { get; set; }
    }
}
