using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class serverContext : DbContext
    {
        public serverContext (DbContextOptions<serverContext> options)
            : base(options)
        {
        }

        public DbSet<server.Models.Product> Product { get; set; } = default!;

        public DbSet<server.Models.Brand> Brand { get; set; } = default!;

        public DbSet<server.Models.Address> Address { get; set; } = default!;

        public DbSet<server.Models.Order> Order { get; set; } = default!;

        public DbSet<server.Models.Order_detail> Order_detail { get; set; } = default!;

        public DbSet<server.Models.User> User { get; set; } = default!;
    }
}
