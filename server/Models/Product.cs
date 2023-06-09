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


        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [StringLength(300)]
        public string Image { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [StringLength(100)]
        public string Gift { get; set; }

        [StringLength(100)]
        public string Model { get; set; }

        [StringLength(20)]
        public string Warranty { get; set; }

        [StringLength(500)]
        public string Description { get; set; }


    }
}
