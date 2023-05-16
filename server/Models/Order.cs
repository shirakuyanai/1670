using System;
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
        public DateTime OrderDate { get; set; }

        [BsonElement("lastUpdate")]
        public DateTime LastUpdate { get; set; }

        [BsonElement("items")]
        public OrderItem[] Items { get; set; }

        [BsonElement("total")]
        public decimal Total { get; set; }
    }

    public class OrderItem
    {
        [BsonElement("product")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }
    }
}
