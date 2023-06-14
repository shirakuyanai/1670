using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class Brand
    {
        [Key]
        public int Bid { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
    }
}
