using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace OnlineShop
{
    public  class OnlineShopContext:DbContext
    {
        public DbSet<Product> products { get; set; }
        public DbSet<CartItem> cartItems { get; set; }
        public DbSet<ShoppingCart> shopingCarts { get; set; }
        public DbSet<Category> categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            string connectionString = configBuilder.GetConnectionString("ShopDatabase");

            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(c => c.Category)
                .HasConversion<string>();

            modelBuilder.Entity<Category>()
                .Property(x => x.Name)
                .HasConversion<string>();
        }
        

    }
}
