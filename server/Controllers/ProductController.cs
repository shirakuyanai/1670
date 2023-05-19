using Microsoft.AspNetCore.Mvc;
using server.Models;
using System.Diagnostics;
using MongoDB.Driver;
using MongoDB.Bson;

namespace server.Controllers
{
    public class ProductController : Controller
    {
        public static string connectionString = "mongodb+srv://react:React123@react.psjcoby.mongodb.net/?retryWrites=true&w=majority";
        public static string databaseName = "test";
        public static IMongoClient client = new MongoClient(connectionString);
        public static IMongoDatabase database = client.GetDatabase(databaseName);
        public static IMongoCollection<Brand> brand_collection = database.GetCollection<Brand>("brands");
        public static IMongoCollection<User> user_collection = database.GetCollection<User>("users");
        public static IMongoCollection<Order> order_collection = database.GetCollection<Order>("orders");
        public static IMongoCollection<OrderDetail> orderdetail_collection = database.GetCollection<OrderDetail>("orderdetails");
        public static IMongoCollection<Product> product_collection = database.GetCollection<Product>("products");
          
        [Route("products")]
        [HttpGet]
        public IActionResult ViewAllProducts()
        {
            var products = product_collection.Find(Builders<Product>.Filter.Empty).ToList();
            return Ok(products);
        }
       
        [Route("product/create")]
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            product_collection.InsertOne(product);
            return Ok(product);
        }

        [Route("product/delete/{id}")]
        [HttpDelete]
        public IActionResult DeleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var result = product_collection.DeleteOne(filter);
            if (result.DeletedCount > 0)
            {
                return Ok("Product deleted successfully.");
            }
            else
            {
                return NotFound("Product not found.");
            }
        }

        [Route("product/view/{id}")]
        [HttpGet]
        public IActionResult GetProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var product = product_collection.Find(filter).FirstOrDefault();
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound("Product not found.");
            }
        }

        [Route("product/edit/{id}")]
        [HttpPut]
        public IActionResult UpdateProduct(string id, [FromBody] Product updatedProduct)
        {
            updatedProduct.Id = id;
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var result = product_collection.ReplaceOne(filter, updatedProduct);
            
            if (result.ModifiedCount > 0)
            {
                return Ok(updatedProduct);
            }
            else
            {
                return NotFound("Product not found.");
            }
        }
    }
}