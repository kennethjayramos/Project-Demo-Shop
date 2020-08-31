using Demo_Shop.Core.Models;
using System.Data.Entity;

namespace Demo_Shop.DataAccess.Sql
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base ("DefaultConnection")
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<BasketItem> BasketItems { get; set; }
    }
}