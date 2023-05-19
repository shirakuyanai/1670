using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace server.Models{
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("username")]
    public string Username { get; set; }

    [BsonElement("password")]
    public string Password { get; set; }

    [BsonElement("phone")]
    public string Phone { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("address")]
    public string Address { get; set; }

    [BsonElement("roles")]
    public string[] Roles { get; set; } = new string[] { "ROLE_USER" };
    [BsonElement("__v")]
        public int Version { get; set; } = 0;
}
}