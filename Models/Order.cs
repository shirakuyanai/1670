using Microsoft.EntityFrameworkCore.Metadata.Internal;
using server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class Order
    {
        [Key]
        public string Order_id { get; set; }

        [ForeignKey("User")]
        public string Uid { get; set; }
        public User User { get; set; }

        [ForeignKey("Address")]
        public string Address_id { get; set; }
        public Address Address { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public int Status { get; set; }
        public double Total { get; set; }

    public Order(){
        Order_id = Guid.NewGuid().ToString();
        Created_at = DateTime.Now;
        Updated_at = DateTime.MinValue;
        Status = 1;
        // 1: Pendding
        // 2: Processing
        // 3: Shipped
        // 4: Delivered
        // 0: Canceled
        Total = 0;
    }
        
    }
}
