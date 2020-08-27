using Demo_Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace Demo_Shop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;

        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;

            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        #region Endpoints
        // Insert
        public void Insert(Product p)
        {
            products.Add(p);
        }

        //Update
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }

            else
            {
                throw new Exception("Product Not Found");
            }
        }

        //Find single product
        public Product Find (string id)
        {
            Product productToFind = products.Find(p => p.Id == id);

            if (productToFind != null)
            {
                return productToFind;
            }

            else
            {
                throw new Exception("Product Not Found");
            }
        }

        //Return a list of queryable products
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        //Delete a product
        public void Delete (string id)
        {
            Product productToDelete = products.Find(p => p.Id == id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }

            else
            {
                throw new Exception("Product Not Found");
            }
        }
        #endregion
    }
}