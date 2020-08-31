using System.Collections.Generic;

namespace Demo_Shop.Core.Models
{
    public class Basket : BaseEntity
    {
        // Lay loading of basktitems
        public virtual ICollection<BasketItem> BasketItems { get; set; }

        #region Constructor
        public Basket()
        {
            this.BasketItems = new List<BasketItem>();
        }
        #endregion
    }
}