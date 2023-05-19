using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace server.Models
{
public class Order

{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("user")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    [BsonElement("orderDate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime OrderDate { get; set; }

    [BsonElement("lastUpdate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime LastUpdate { get; set; }

    [BsonElement("orderDetail")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string OrderDetailId { get; set; }

    [BsonElement("status")]
    public int Status { get; set; }
    [BsonElement("__v")]
        public int Version { get; set; } = 0;
}
}