using Demo_Shop.Core.Contracts;
using Demo_Shop.Core.Models;
using Demo_Shop.Core.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_Shop.Controllers
{
    public class ProductManagementController : Controller
    {
        //create the instance of the ProductRepository & ProductCategoryRepository
        IRepository<Product> context;

        IRepository<ProductCategory> productCategories;

        #region Construtor
        public ProductManagementController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;

            productCategories = productCategoryContext;
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
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();

            viewModel.ProductCategories = productCategories.Collection();

            return View(viewModel);
        }

        // POST: Product
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase image)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }

            else
            {
                if (image != null && image.ContentLength > 0)
                {
                    string uploadPath = Server.MapPath("~/ProductImages/");

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    product.Image = product.Id + Path.GetExtension(image.FileName);

                    image.SaveAs(uploadPath + product.Image);
                }

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
                ProductManagerViewModel viewModel = new ProductManagerViewModel();

                viewModel.Product = productToEdit;

                viewModel.ProductCategories = productCategories.Collection();

                return View(viewModel);
            }
        }

        // PUT: Product
        [HttpPost]
        public ActionResult Edit(Product product, string id, HttpPostedFileBase image)
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
                    if (image != null && image.ContentLength > 0)
                    {
                        string uploadPath = Server.MapPath("~/ProductImages/");

                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        else
                        {
                            if (productToEdit.Image != null)
                            {
                                var oldImage = Request.MapPath("~/ProductImages/" + productToEdit.Image);

                                System.IO.File.Delete(oldImage);
                            }                            

                            productToEdit.Image = product.Id + Path.GetExtension(image.FileName);

                            image.SaveAs(uploadPath + productToEdit.Image);
                        }                        
                    }

                    productToEdit.Category = product.Category;

                    productToEdit.Description = product.Description;

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
                if (productToDelete.Image != null)
                {
                    var productImage = Request.MapPath("~/ProductImages/" + productToDelete.Image);

                    System.IO.File.Delete(productImage);
                }

                context.Delete(id);

                context.Commit();

                return RedirectToAction("Index", "ProductManagement");
            }
        }
    }
}