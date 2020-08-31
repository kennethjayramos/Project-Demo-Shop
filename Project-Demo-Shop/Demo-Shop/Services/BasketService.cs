using Demo_Shop.Core.Contracts;
using Demo_Shop.Core.Models;
using System;
using System.Linq;
using System.Web;

namespace Demo_Shop.Services
{
    public class BasketService
    {
        IRepository<Product> productContext;

        IRepository<Basket> basketContext;

        public const string BasketSessionName = "DemoShopBasket";

        #region Constructor
        public BasketService(IRepository<Product> ProductContext, IRepository<Basket> BasketContext)
        {
            this.basketContext = BasketContext;

            this.productContext = ProductContext;
        }
        #endregion

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);

            Basket basket = new Basket();

            if (cookie != null)
            {
                string basketId = cookie.Value;

                if (string.IsNullOrEmpty(basketId))
                {
                    basket = basketContext.Find(basketId);
                }

                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }
                }
            }

            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }

            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();

            basketContext.Insert(basket);

            basketContext.Commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName);

            cookie.Value = basket.Id;

            cookie.Expires = DateTime.Now.AddHours(3);

            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }

        public void AddToBasket(HttpContextBase httpContext, string productId)
        {
            Basket basket = GetBasket(httpContext, true);

            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.Id,

                    ProductId = productId,

                    Quantity = 1
                };

                basket.BasketItems.Add(item);
            }

            else
            {
                item.Quantity = item.Quantity + 1;
            }

            basketContext.Commit();
        }

        public void RemoveFromBasket(HttpContextBase httpContext, string basketItemId)
        {
            Basket basket = GetBasket(httpContext, true);

            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == basketItemId);

            if (item != null)
            {
                basket.BasketItems.Remove(item);

                basketContext.Commit();
            }
        }
    }
}