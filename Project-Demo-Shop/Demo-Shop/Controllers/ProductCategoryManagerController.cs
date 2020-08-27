using Demo_Shop.Core.Models;
using Demo_Shop.DataAccess.InMemory;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Demo_Shop.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        // create the instance of the product category repository
        InMemoryRepository<ProductCategory> context;

        #region Constructor
        public ProductCategoryManagerController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }
        #endregion

        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();

            return View(productCategories);
        }

        // POST: ProductCategoryManager
        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();

            return View(productCategory);
        }

        // POST: ProductCategory
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }

            else
            {
                context.Insert(productCategory);

                context.Commit();

                return RedirectToAction("Index", "ProductCategoryManager");
            }
        }

        // PUT: ProductCategoryManager
        public ActionResult Edit(string id)
        {
            ProductCategory productCategoryToEdit = context.Find(id);

            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(productCategoryToEdit);
            }
        }

        // PUT: ProductCategory
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory,string id)
        {
            ProductCategory productCategoryToEdit = context.Find(id);

            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategoryToEdit);
                }

                else
                {
                    productCategoryToEdit.Name = productCategory.Name;

                    context.Commit();

                    return RedirectToAction("Index", "ProductCategoryManager");
                }
            }
        }

        // DELETE: ProductCategoryManager
        public ActionResult Delete(string id)
        {
            ProductCategory productCategoryToDelete = context.Find(id);

            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(productCategoryToDelete);
            }
        }

        // DELETE: ProductCategory
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            ProductCategory productCategoryToDelete = context.Find(id);

            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                context.Delete(id);

                context.Commit();

                return RedirectToAction("Index", "ProductCategoryManager");
            }
        }
    }
}