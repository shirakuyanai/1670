using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
}