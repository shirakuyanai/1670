using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class Order_detail
    {
        [Key]
        public string Order_items_id { get; set; }

        [ForeignKey("Order")]
        public string Order_id { get; set; }
        public Order Order { get; set; }


        [Required]
        [ForeignKey("Product")]
        public int Pid { get; set; }
        public Product Product { get; set; }

        public double quantity { get; set; }
        public double total { get; set; }
        public Order_detail(){
            Order_items_id = Guid.NewGuid().ToString();
        }
    }
}
