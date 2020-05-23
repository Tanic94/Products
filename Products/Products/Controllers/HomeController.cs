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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Product()
        {
            IQueryable<Product> products = dbConnecion.Products.OrderBy(p => p.Name);
            return View(products);
        }
    }
}