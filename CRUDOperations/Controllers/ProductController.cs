using CRUDOperations.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDOperations.Controllers
{
    public class ProductController : Controller
    {
        public object GlobalClass { get; private set; }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewAll()
        {
            return View(GetAllProducts());
        }

        IEnumerable<Products> GetAllProducts()
        {
            using (Northwind _context = new Northwind())
            {
                return _context.Products.ToList();
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            var product = new Products();
            if (id != 0)
            {
                using (Northwind db = new Northwind())
                {
                    product = db.Products.Where(x => x.ProductID == id).FirstOrDefault();
                }
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Products product)
        {
           
                if (product.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
                    string extension = Path.GetExtension(product.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    product.ImagePath = "~/AppFiles/Images/" + fileName;
                    product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }
                using (Northwind db = new Northwind())
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                return RedirectToAction("ViewAll");
            }
       }
    
}