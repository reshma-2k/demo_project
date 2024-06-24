using SchoolManagementDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementDb.Controllers
{
    public class PrincipalController : Controller
    {
        // GET: Principal
        SchoolContext db = new SchoolContext();
        // Student Management
        public ActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("ViewStudents");
            }
            return View(student);
        }
        public ActionResult ViewStudents()
        {
            var students = db.Students.ToList();
            return View(students);
        }
        //teacher management

        public ActionResult AddTeacher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTeacher(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("ViewTeachers");
            }
            return View(teacher);
        }
        public ActionResult ViewTeachers()
        {
            var teachers = db.Teachers.ToList();
            return View(teachers);
        }
      
        public ActionResult Dashboard()
        {
            var classMostExcellent = db.Students
                .Where(s => s.EducationalStatus == "Excellent")
                .GroupBy(s => s.Class)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            var classMostAverage = db.Students
                .Where(s => s.EducationalStatus == "Average")
                .GroupBy(s => s.Class)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            var classMostPoor = db.Students
                .Where(s => s.EducationalStatus == "Poor")
                .GroupBy(s => s.Class)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            ViewBag.ClassMostExcellent = classMostExcellent;
            ViewBag.ClassMostAverage = classMostAverage;
            ViewBag.ClassMostPoor = classMostPoor;

            return View();
        }

    }
}