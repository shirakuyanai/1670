using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class Product
    {
        [Key]
        public int Pid { get; set; }

        [Required]
        [ForeignKey("Brand")]
        public int Bid { get; set; }
        public Brand Brand { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The Price of product must be greater than or equal to 0.")]
        public double Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The Stock of product must be greater than or equal to 0.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "The Image field is required.")]
        public string Image { get; set; }

        public string Color { get; set; }

        public string Gift { get; set; }

        public string Model { get; set; }

        public string Warranty { get; set; }
        public string Description { get; set; }


    }
}
