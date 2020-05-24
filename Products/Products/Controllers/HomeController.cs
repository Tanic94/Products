using Newtonsoft.Json;
using Products.Models;
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

        public ActionResult ListOfArticles(){
            var json = "{\"articles\":[{\"ArticleId\":\"Id1\", \"ArticleName\":\"Name1\", \"ArticleDescription\":\"Description1\", \"ArticleCategory\":\"Category1\", \"ArticleManufacturer\":\"Manufacturer1\", \"ArticleSupplier\":\"Supplier1\", \"ArticlePrice\":\"Price1\"},{\"ArticleId\":\"Id2\", \"ArticleName\":\"Name2\", \"ArticleDescription\":\"Description2\", \"ArticleCategory\":\"Category2\", \"ArticleManufacturer\":\"Manufacturer2\", \"ArticleSupplier\":\"Supplier2\", \"ArticlePrice\":\"Price2\"}]}";
            var articles = JsonConvert.DeserializeObject<Articles>(json);
            return View(articles);
        }

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
                    /*pretpostavimo da je cena celobrojna vrednost*/
                    newProduct.Price = int.Parse(priceOfProduct);
                    dbConnecion.Products.Add(newProduct);
                }
                dbConnecion.SaveChanges();
            }
            return RedirectToAction("Product");
        }
        public ActionResult DeleteProduct(string productId)
        {
            var productGuid = Guid.Parse(productId);
            var productToDelete = dbConnecion.Products.FirstOrDefault(p => p.ProductID == productGuid);
            if (productToDelete != null)
            {
                dbConnecion.Products.Remove(productToDelete);
                dbConnecion.SaveChanges();
            }
            return RedirectToAction("Product");
        }
        public ActionResult EditProduct(string productId, string newNameOfProduct, string newDescriptionOfProduct, string newCategoryOfProduct,
            string newManufacturerOfProduct, string newSupplierOfProduct, string newPriceOfProduct)
        {
            var productGuid = Guid.Parse(productId);
            var productToEdit = dbConnecion.Products.FirstOrDefault(p => p.ProductID == productGuid);
            if (productToEdit != null)
            {
                
                    if (newPriceOfProduct != "" && newDescriptionOfProduct != "" && newCategoryOfProduct != "" && newManufacturerOfProduct != "" &&
                        newSupplierOfProduct != "" && newPriceOfProduct != "")
                    {
                        productToEdit.Name = newNameOfProduct;
                        productToEdit.Description = newDescriptionOfProduct;
                        productToEdit.Category = newCategoryOfProduct;
                        productToEdit.Manufacturer = newManufacturerOfProduct;
                        productToEdit.Supplier = newSupplierOfProduct;
                        productToEdit.Price = int.Parse(newPriceOfProduct);
                    }
                
            }
            dbConnecion.SaveChanges();
            return RedirectToAction("Product");
        }
    }
}