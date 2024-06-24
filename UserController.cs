using SchoolManagementDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementDb.Controllers
{
    public class UserController : Controller
    {
        private SchoolContext db = new SchoolContext();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user =db.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                Session["Role"] = user.Username;
                Session["Role"] = user.Password;
               

                if (user.Role == "Principal")
                {
                    return RedirectToAction("Dashboard", "Principal");
                }
                else if (user.Role == "Teacher")
                {
                    return RedirectToAction("Dashboard", "Teacher");
                }
                else if (user.Role == "Student")
                {
                    return RedirectToAction("Dashboard", "Student");
                }
            }
            ViewBag.Message = "Invalid username or password";
            return View();
        }
    }
}