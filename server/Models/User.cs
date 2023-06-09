using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class User
    {
        [Key]
        public int Uid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
    }
}
