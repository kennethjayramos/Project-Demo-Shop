using Demo_Shop.Services;
using System.Web.Mvc;

namespace Demo_Shop.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketService;

        #region
        public BasketController(IBasketService BasketService)
        {
            this.basketService = BasketService;
        }
        #endregion

        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);

            return View(model);
        }

        // Add Product item into Basket
        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        // Remove Product item from Basket
        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        // Display Basket Summary as PartialView
        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummary);
        }
    }
}