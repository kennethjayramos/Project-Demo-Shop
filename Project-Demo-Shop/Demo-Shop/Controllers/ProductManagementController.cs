using Demo_Shop.Core.Models;
using Demo_Shop.DataAccess.InMemory;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Demo_Shop.Controllers
{
    public class ProductManagementController : Controller
    {
        //create the instance of the product repository
        ProductRepository context;

        #region Construtor
        public ProductManagementController()
        {
            context = new ProductRepository();
        }
        #endregion

        // GET: ProductManagement
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();

            return View(products);
        }

        // POST: ProductManagement
        public ActionResult Create()
        {
            Product product = new Product();

            return View(product);
        }

        // POST: Product
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }

            else
            {
                context.Insert(product);

                context.Commit();

                return RedirectToAction("Index", "ProductManagement");
            }
        }

        // PUT: ProductManagement
        public ActionResult Edit(string id)
        {
            Product productToEdit = context.Find(id);

            if(productToEdit == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(productToEdit);
            }
        }

        // PUT: Product
        [HttpPost]
        public ActionResult Edit(Product product, string id)
        {
            Product productToEdit = context.Find(id);

            if(productToEdit == null)
            {
                return HttpNotFound();
            }

            else
            {
                if(!ModelState.IsValid)
                {
                    return View(productToEdit);
                }

                else
                {
                    productToEdit.Category = product.Category;

                    productToEdit.Description = product.Description;

                    productToEdit.Image = product.Image;

                    productToEdit.Name = product.Name;

                    productToEdit.Price = product.Price;

                    context.Commit();

                    return RedirectToAction("Index", "ProductManagement");
                }
            }
        }

        // DELETE: ProductManagement
        public ActionResult Delete(string id)
        {
            Product productToDelete = context.Find(id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(productToDelete);
            }
        }

        // DELETE: Product
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            Product productToDelete = context.Find(id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                context.Delete(id);

                context.Commit();

                return RedirectToAction("Index", "ProductManagement");
            }
        }
    }
}