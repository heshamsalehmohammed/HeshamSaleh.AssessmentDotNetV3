using Flunt.Notifications;
using HeshamSaleh.AssessmentDotNetV3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace HeshamSaleh.AssessmentDotNetV3.Infrastructure.Context
{
    public class DBContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
            // seed the database with dummy data
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    Name = "Short",
                    Price = "25EGP",
                    Quantity = 5,
                    ImgURL = "//",
                    CategoryId = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b")
                },
                new Product()
                {
                    Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    Name = "T-Shirt",
                    Price = "25EGP",
                    Quantity = 4,
                    ImgURL = "//",
                    CategoryId = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee")
                }
                );

            modelBuilder.Entity<Category>().HasData(
               new Category
               {
                   Id = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
                   Name = "Commandeering a Ship Without Getting Caught",
               },
               new Category
               {
                   Id = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
                   Name = "Overthrowing Mutiny",
               }
               );

            base.OnModelCreating(modelBuilder);
        }
    }
}
