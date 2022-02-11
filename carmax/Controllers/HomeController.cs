using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using carmax.Models;
namespace carmax.Controllers
{
    public class HomeController : Controller
    {
        carmaxEntities1 db = new carmaxEntities1();
        public ActionResult Index()
        {
            
            List<product> car = db.products.Take(3).ToList();

            return View(car);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterData(string username,string email,string phone,string password)
        {
            int resCount = db.logins.Where(temp => temp.email.Equals(email)).Count();
            if(resCount>0)
            {
                ViewBag.Message = "The email already exist! Please log in.";
                return View("Login");
            }
            else
            {
                System.Security.Cryptography.MD5CryptoServiceProvider test123 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
                data = test123.ComputeHash(data);
                String newPassword = System.Text.Encoding.ASCII.GetString(data);
                login lg = new login();
                lg.username = username;
                lg.email = email;
                lg.phone = phone;
                lg.type = "normal";
                lg.password = newPassword;
                db.logins.Add(lg);
                db.SaveChanges();

                ViewBag.Message = "Register completed! Please log in.";
                return View("Login");
            }
            
        }
        [HttpPost]
        public ActionResult LoginData(string email,string password)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider test123 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = test123.ComputeHash(data);
            String newPassword = System.Text.Encoding.ASCII.GetString(data);
            int resCount = db.logins.Where(temp => temp.email.Equals(email) & temp.password.Equals(newPassword)).Count();
            if(resCount > 0)
            {
                List<login> user = db.logins.Where(temp=> temp.email.Equals(email) ).ToList();
                foreach (var i in user)
                {
                    Session["username"] = i.username;
                    Session["userType"] = i.type;
                }

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Email or Password does not match! Try again.";
                return View("Login");
            }
        }
        public ActionResult Login()
        {

            return View();
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