using SchoolManagementDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementDb.Controllers
{
   
    public class TeacherController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Teacher/Login
        public ActionResult Login()
        {
            return View(new TeacherLoginViewModel());
        }

        // POST: Teacher/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TeacherLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var teacher = db.Teachers
                                    .FirstOrDefault(t => t.Username == model.Username && t.Password == model.Password);

                    if (teacher != null)
                    {
                        Session["Username"] = teacher.Username;
                        Session["Password"] = teacher.Password;
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password.");
                    }
                }
                catch (Exception ex)
                {
                    // Log the detailed error
                    Console.WriteLine("Error: " + ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                    }
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                }
            }
            return View(model);
        }

        // GET: Teacher/Dashboard
        public ActionResult Dashboard()
        {
            if (Session["TeacherID"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        // GET: Teacher/ViewStudents
        public ActionResult ViewStudents()
        {
            if (Session["TeacherID"] == null)
            {
                return RedirectToAction("Login");
            }

            int teacherID = (int)Session["TeacherID"];
            var students = db.Students.Where(s => s.StudentId == teacherID).ToList();
            return View(students);
        }

        // GET: Teacher/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}