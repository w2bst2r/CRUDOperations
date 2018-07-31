using CRUDOperations.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUDOperations;

namespace CRUDOperations.Controllers
{
    public class ProductController : Controller
    {

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
            try
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
                    if (product.ProductID == 0)
                    {
                        db.Products.Add(product);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllProducts()), message = "submitted successfuly" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (Northwind db = new Northwind())
                {
                    Products product = db.Products.Where(x => x.ProductID == id).FirstOrDefault();
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllProducts()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}  
