using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using carmax.Models;
using System.IO;

namespace carmax.Controllers
{
    public class AdminController : Controller
    {
        carmaxEntities1 db = new carmaxEntities1();
        // GET: Admin
        public ActionResult Index()
        {
            return Content("hello");
        }
        public ActionResult Create()
        {
            return View();
        }
        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Max_power,Seating_capacity,Body_type,Fuel_type,No_of_cylinder,Color,Engine_type,Engine_displacement,Type,Image,Brand,Model,Year,Discount,File")] product car)
        {
            if (ModelState.IsValid)
            {
                var filename = Path.GetFileName(car.File.FileName);
                string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                car.img = "~/Images/" + _filename;

                db.products.Add(car);
                if (db.SaveChanges() > 0)
                {
                    car.File.SaveAs(path);
                }
                return RedirectToAction("Index");
            }

            return View(car);
        }
    }
}