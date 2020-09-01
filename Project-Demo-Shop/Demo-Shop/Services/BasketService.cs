using Demo_Shop.Core.Contracts;
using Demo_Shop.Core.Models;
using Demo_Shop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Shop.Services
{
    public class BasketService : IBasketService
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

                if (!string.IsNullOrEmpty(basketId))
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

        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext)
        {
            // Get the basket from the db
            Basket basket = GetBasket(httpContext, false);

            // If the basket is retrieved query the product table
            if (basket != null)
            {
                var results = (from b in basket.BasketItems
                               join p in productContext.Collection() on b.ProductId equals p.Id
                               select new BasketItemViewModel()
                               {
                                   Id = b.Id,
                                   Quantity = b.Quantity,
                                   ProductName = p.Name,
                                   Image = p.Image,
                                   Price = p.Price
                               }).ToList();

                return results;
            }

            // Return an empty list of basketItems
            else
            {
                return new List<BasketItemViewModel>();
            }
        }

        public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext)
        {
            // Get the basket from the db
            Basket basket = GetBasket(httpContext, false);

            BasketSummaryViewModel model = new BasketSummaryViewModel(0, 0);

            if (basket != null)
            {
                // nullable variable for counting basket items
                int? basketCount = (from item in basket.BasketItems
                                    select item.Quantity).Sum();

                // nullable variable for total price of basket items
                decimal? basketTotal = (from item in basket.BasketItems
                                        join p in productContext.Collection() on item.ProductId equals p.Id
                                        select item.Quantity * p.Price).Sum();

                // Assign the query results to the model
                model.BasketCount = basketCount ?? 0;

                model.BasketTotal = basketTotal ?? decimal.Zero;

                return model;
            }

            else
            {
                return model;
            }
        }
    }
}