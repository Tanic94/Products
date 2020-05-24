using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Products.Controllers
{
    public class HomeController : Controller
    {
        ProductDbEntities dbConnecion = new ProductDbEntities();

        public ActionResult Product()
        {
            IQueryable<Product> products = dbConnecion.Products.OrderBy(p => p.Name);
            return View(products);
        }
        public ActionResult AddProduct(string nameOfProduct, string descriptionOfProduct, string categoryOfProduct, string manufacturerOfProduct,
            string supplierOfProduct, String priceOfProduct)
        {
            if (nameOfProduct != "" && descriptionOfProduct != "" && categoryOfProduct != "" && manufacturerOfProduct != "" && supplierOfProduct != "" && priceOfProduct !="") {
                IQueryable<Product> returnProducts = dbConnecion.Products.Where(p => p.Name.Equals(nameOfProduct));
                if (returnProducts.Count() == 0)
                {

                    Product newProduct = new Product();
                    newProduct.ProductID = System.Guid.NewGuid();
                    newProduct.Name = nameOfProduct;
                    newProduct.Description = descriptionOfProduct;
                    newProduct.Category = categoryOfProduct;
                    newProduct.Manufacturer = manufacturerOfProduct;
                    newProduct.Supplier = supplierOfProduct;
                    newProduct.Price = int.Parse(priceOfProduct);
                    dbConnecion.Products.Add(newProduct);
                }
                dbConnecion.SaveChanges();
            }
            return RedirectToAction("Product");
        }
    }
}