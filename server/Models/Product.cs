using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace server.Models{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("brand")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BrandId { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("stock")]
        public int Stock { get; set; }

        [BsonElement("image")]
        public string Image { get; set; }

        [BsonElement("color")]
        public string Color { get; set; }

        [BsonElement("model")]
        public string Model { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("__v")]
        public int Version { get; set; } = 0;
    }
}