using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace server.Models
{
public class OrderDetail
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("order")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string OrderId { get; set; }

    [BsonElement("user")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    [BsonElement("quantity")]
    public int Quantity { get; set; }
    [BsonElement("__v")]
        public int Version { get; set; } = 0;
}
}