using Microsoft.AspNetCore.Mvc;
using server.Models;
using System.Diagnostics;
using MongoDB.Driver;
using MongoDB.Bson;

namespace server.Controllers
{
    public class HomeController : Controller
    {
        private static string connectionString = "mongodb+srv://react:React123@react.psjcoby.mongodb.net/?retryWrites=true&w=majority";
        private static string databaseName = "test";
        private static IMongoClient client = new MongoClient(connectionString);
        private static IMongoDatabase database = client.GetDatabase(databaseName);
        private static IMongoCollection<Brand> brand_collection = database.GetCollection<Brand>("brands");
        private static IMongoCollection<Brand> user_collection = database.GetCollection<Brand>("users");
        private static IMongoCollection<Brand> order_collection = database.GetCollection<Brand>("orders");
        private static IMongoCollection<Brand> product_collection = database.GetCollection<Brand>("products");

        [Route("brands")]
        [HttpGet]
        public IActionResult GetBrands()
        {
            var filter = Builders<Brand>.Filter.Empty; // Empty filter to fetch all documents
            var brands = brand_collection.Find(filter).ToList();
            return Json(brands);
        }

        [Route("brand/create")]
        [HttpPost]
        public IActionResult CreateBrand([FromBody] Brand brand)
        {
            brand_collection.InsertOne(brand);
            return Json(brand);
        }

        [Route("brand/delete/{id}")]
        [HttpDelete]
        public IActionResult DeleteBrand(string id)
        {
            ObjectId objectId;
            if (!ObjectId.TryParse(id, out objectId))
            {
                return BadRequest("Invalid id format");
            }
            
            var filter = Builders<Brand>.Filter.Eq("_id", objectId);
            Brand brand = brand_collection.Find(filter).Limit(1).FirstOrDefault();
            brand_collection.DeleteOne(filter);
            return Json(brand);
        }

        [Route("brand/view/{id}")]
        [HttpGet]
        public IActionResult GetBrand(string id)
        {
            ObjectId objectId;
            if (!ObjectId.TryParse(id, out objectId))
            {
                return BadRequest("Invalid id format");
            }
            
            var filter = Builders<Brand>.Filter.Eq("_id", objectId);
            Brand brand = brand_collection.Find(filter).Limit(1).FirstOrDefault();
            return Json(brand);
        }

        [Route("brand/edit/{id}")]
        [HttpPut]
        public IActionResult UpdateBrand(string id, [FromBody] Brand newBrand)
        {
            ObjectId objectId;
            if (!ObjectId.TryParse(id, out objectId))
            {
                return BadRequest("Invalid id format");
            }

            var filter = Builders<Brand>.Filter.Eq("_id", objectId);
            Brand existingBrand = brand_collection.Find(filter).FirstOrDefault();

            if (existingBrand != null)
            {
                existingBrand.Name = newBrand.Name;

                brand_collection.ReplaceOne(filter, existingBrand);

                return Json(existingBrand);
            }
            else
            {
                return NotFound();
            }
        }
    }
}