using System.ComponentModel.DataAnnotations;

namespace server.Areas.Staff.Models
{
    public class Brand
    {
        [Key]
        public int Bid { get; set; }
        public string Title { get; set; }
    }
}
