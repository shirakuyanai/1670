﻿using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Infrastructure;
using server.Models;

namespace server.Infrastructure
{
    public class SeedData
    {
        public static void SeedDatabase(serverContext context)
        {
            context.Database.Migrate();

            if (!context.Product.Any())
            {
                Brand A = new Brand { Title = "Nguyen Luong =" };
                Brand B = new Brand { Title = "Pham Minh Hiu" };

                context.Product.AddRange(
                        new Product
                        {
                            Brand = A,
                            Name = "Acer nitro 5",
                            Price = 151,
                            Stock = 4,
                            Image = "jkbweqhj.png",
                            Color = "red",
                            Gift = "cdb",
                            Model = "flag ship",
                            Warranty = "2 years",
                            Description = "yrhrtfvbdb"
                        },

                        new Product
                        {
                            Brand = A,
                            Name = "Mac M1",
                            Price = 133,
                            Stock = 2,
                            Image = "jkbweqhj.png",
                            Color = "brown",
                            Gift = "cdb",
                            Model = "flag ship",
                            Warranty = "2 years",
                            Description = "yrhrtfvbdb"
                        },

                        new Product
                        {
                            Brand = B,
                            Name = "Asus vivobook",
                            Price = 10000,
                            Stock = 10,
                            Image = "jkbweqhj.png",
                            Color = "pink",
                            Gift = "cdb",
                            Model = "flag ship",
                            Warranty = "2 years",
                            Description = "yrhrtfvbdb"
                        },

                        new Product
                        {
                            Brand = B,
                            Name = "Dell G15",
                            Price = 7878,
                            Stock = 0,
                            Image = "jkbweqhj.png",
                            Color = "green",
                            Gift = "cdb",
                            Model = "flag ship",
                            Warranty = "2 years",
                            Description = "yrhrtfvbdb"
                        }
                );

                context.SaveChanges();
            }
        }
    }
}