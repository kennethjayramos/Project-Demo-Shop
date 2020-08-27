using Demo_Shop.Core.Models;
using System.Collections.Generic;

namespace Demo_Shop.Core.ViewModels
{
    public class ProductManagerViewModel
    {
        public Product Product { get; set; }

        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}