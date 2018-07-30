using CRUDOperations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDOperations.Controllers
{
    public class HomeController : Controller
    {
        Northwind _context;

        public ActionResult Index()
        {
            _context = new Northwind();
            var customerTable = _context.Customers.ToList();
            return View(customerTable);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}