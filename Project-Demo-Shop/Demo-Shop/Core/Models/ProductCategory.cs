using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Shop.Core.Models
{
    public class ProductCategory
    {
        public string Id { get; set; }

        public string Name { get; set; }

        #region Construtor (ProductCategory Id)
        public ProductCategory()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        #endregion
    }
}