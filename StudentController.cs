using SchoolManagementDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementDb.Controllers
{
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Student/Login
        public ActionResult Login()
        {
            return View(new StudentLoginViewModel());
        }

        // POST: Student/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(StudentLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Student = db.Students.FirstOrDefault(s => s.Username == model.Username && s.Password == model.Password);

                if (Student != null)
                {
                    Session["Username"] = Student.Username;
                    Session["Password"] = Student.Password;
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }

        // GET: Student/Dashboard
        public ActionResult Dashboard()
        {
            if (Session["StudentID"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        // GET: Student/ViewMotivation
        public ActionResult ViewMotivation()
        {
            if (Session["StudentID"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        // GET: Student/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}