using Demo_Shop.Core.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace Demo_Shop.Services
{
    public interface IBasketService
    {
        void AddToBasket(HttpContextBase httpContext, string productId);

        List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext);

        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext);

        void RemoveFromBasket(HttpContextBase httpContext, string basketItemId);
    }
}