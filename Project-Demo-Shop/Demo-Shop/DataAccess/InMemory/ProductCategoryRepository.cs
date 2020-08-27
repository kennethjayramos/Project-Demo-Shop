using Demo_Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace Demo_Shop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;

        List<ProductCategory> productCategories;

        #region Constructor
        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;

            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }
        #endregion

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        #region Endpoints
        // Insert
        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        // Update
        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }

            else
            {
                throw new Exception("Product Category Not Found");
            }
        }

        // Find Single Product Category
        public ProductCategory Find(string id)
        {
            ProductCategory productCategoryToFind = productCategories.Find(p => p.Id == id);

            if (productCategoryToFind != null)
            {
                return productCategoryToFind;
            }

            else
            {
                throw new Exception("Product Category Not Found");
            }
        }

        // Return a list of queryable product categories
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        //Delete a product category
        public void Delete(string id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == id);

            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }

            else
            {
                throw new Exception("Product Category Not Found");
            }
        }
        #endregion
    }
}