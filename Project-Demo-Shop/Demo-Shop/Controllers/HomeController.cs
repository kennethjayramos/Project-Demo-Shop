using Demo_Shop.Core.Contracts;
using Demo_Shop.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Demo_Shop.Controllers
{
    public class HomeController : Controller
    {
        //create the instance of the ProductRepository & ProductCategoryRepository
        IRepository<Product> context;

        IRepository<ProductCategory> productCategories;

        #region Construtor
        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;

            productCategories = productCategoryContext;
        }
        #endregion

        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();

            return View(products);
        }

        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);

            if (product == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(product);
            }
        }
    }
}