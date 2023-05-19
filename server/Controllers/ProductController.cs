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
        public IActionResult GetProduct()
        {
            var filter = Builders<Product>.Filter.Empty; // Empty filter to fetch all documents
            var products = product_collection.Find(filter).ToList();
            return Json(products);
        }
       
        [Route("product/create")]
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            product_collection.InsertOne(product);
            return Json(product);
        }

        [Route("product/delete/{id}")]
        [HttpDelete]
        public IActionResult DeleteProduct(string id)
        {
            ObjectId objectId;
            if (!ObjectId.TryParse(id, out objectId))
            {
                return BadRequest("Invalid id format");
            }
            
            var filter = Builders<Product>.Filter.Eq("_id", objectId);
            Product product = product_collection.Find(filter).Limit(1).FirstOrDefault();
            product_collection.DeleteOne(filter);
            return Json(product);
        }

        [Route("product/view/{id}")]
        [HttpGet]
        public IActionResult GetProduct(string id)
        {
            ObjectId objectId;
            if (!ObjectId.TryParse(id, out objectId))
            {
                return BadRequest("Invalid id format");
            }
            
            var filter = Builders<Product>.Filter.Eq("_id", objectId);
            Product product = product_collection.Find(filter).Limit(1).FirstOrDefault();
            return Json(product);
        }

        [Route("product/edit/{id}")]
        [HttpPut]
        public IActionResult UpdateProduct(string id, [FromBody] Product newProduct)
        {
            ObjectId objectId;
            if (!ObjectId.TryParse(id, out objectId))
            {
                return BadRequest("Invalid id format");
            }

            var filter = Builders<Product>.Filter.Eq("_id", objectId);
            Product existingProduct = product_collection.Find(filter).FirstOrDefault();

            if (existingProduct != null)
            {
                existingProduct.Name = newProduct.Name;

                product_collection.ReplaceOne(filter, existingProduct);

                return Json(existingProduct);
            }
            else
            {
                return NotFound();
            }
        }
    }
}