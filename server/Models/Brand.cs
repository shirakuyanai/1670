using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class Brand
    {
        [Key]
        public int Bid { get; set; }
        public string Title { get; set; }
    }
}
