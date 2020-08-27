using System;

namespace Demo_Shop.Core.Models
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        #region Constructor for Id & CreatedAt
        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();

            this.CreatedAt = DateTime.Now;
        }
        #endregion
    }
}