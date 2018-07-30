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
    public class CustomerController : Controller
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
            return View(GetAllCustomers());
        }

        IEnumerable<Customers> GetAllCustomers()
        {
            using (Northwind _context = new Northwind())
            {
                return _context.Customers.ToList();
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(string id = "0")
        {
            var customer = new Customers();
            if (id != "0")
            {
                using (Northwind db = new Northwind())
                {
                    customer = db.Customers.Where(x => x.CustomerID == id).FirstOrDefault();
                }
            }
            return View(customer);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Customers customer)
        {
            //try
            //{
            //    if (customer.ImageUpload != null)
            //    {
            //        string fileName = Path.GetFileNameWithoutExtension(customer.ImageUpload.FileName);
            //        string extension = Path.GetExtension(customer.ImageUpload.FileName);
            //        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            //        customer.ImagePath = "~/AppFiles/Images/" + fileName;
            //        customer.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
            //    }
            //    using (Northwind db = new Northwind())
            //    {
            //        if (customer.CustomerID == "0")
            //        {
            //            db.Customers.Add(customer);
            //            db.SaveChanges();
            //        }
            //        else
            //        {
            //            db.Entry(customer).State = EntityState.Modified;
            //            db.SaveChanges();
            //        }
            //    }
            //    return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllCustomers()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            //}
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}