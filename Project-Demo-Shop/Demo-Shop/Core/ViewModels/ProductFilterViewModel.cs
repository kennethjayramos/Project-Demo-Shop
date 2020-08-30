using Demo_Shop.Core.Models;
using System.Collections.Generic;

namespace Demo_Shop.Core.ViewModels
{
    public class ProductFilterViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}