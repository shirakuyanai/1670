using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("category")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; }

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
    public string[] Image { get; set; }

    [BsonElement("colors")]
    public string[] Colors { get; set; }

    [BsonElement("models")]
    public string[] Models { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }
}