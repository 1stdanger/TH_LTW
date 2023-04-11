using BigSchool.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CourseController : Controller
    {
        BigSchoolContext context = new BigSchoolContext();
        // GET: Course
        public ActionResult Create()
        {
            Cours objcourse = new Cours();
            objcourse.ListCategory = context.Categoryies.ToList();
            return View(objcourse);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cours objcourse)
        {
            ModelState.Remove("LecturerId");
            if(!ModelState.IsValid){
                objcourse.ListCategory = context.Categoryies.ToList();
                return View("Create", objcourse);
            }
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                         .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            objcourse.LecturerId = user.Id;
            context.Courses.Add(objcourse);
            context.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        public ActionResult Attending()
        {
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var listAttendances = context.Attendancees.Where(p => p.Attendee == currentUser.Id).ToList();
            var courses = new List<Cours>();
            foreach(Attendancee temp in listAttendances){
                Cours objCourse = temp.Cours;
                objCourse.Name = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                         .FindById(objCourse.LecturerId).Name;
                courses.Add(objCourse);
            }
            return View(courses);
        }
        public ActionResult Mine()
        {
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var courses = context.Courses.Where(c => c.LecturerId == currentUser.Id && c.DateTime > DateTime.Now).ToList();
            foreach(Cours temp in courses)
            {
                temp.Name = currentUser.Name;
            }
            return View(courses);
        }
        public ActionResult Edit(int id)
        {
            var course = context.Courses.First(x => x.Id == id);
            return View(course);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_course = context.Courses.First(m => m.Id == id);
            var LecturerId = collection["LecturerId"];
            var Place = collection["Place"];
            var DateTime = collection["DateTime"];
            var CategoryId = collection["CategoryId"];
            E_course.Id = id;
            if (string.IsNullOrEmpty(LecturerId))
            {
                ViewData["Error"] = "Không được để trống.";
            }
            else
            {
                E_course.LecturerId = LecturerId;
                E_course.Place = Place;
                E_course.DateTime = Convert.ToDateTime(DateTime);
                E_course.CategoryId = int.Parse(CategoryId);
                UpdateModel(E_course);
                context.SaveChanges();
                return RedirectToAction("Mine");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var dbDelete = context.Courses.First(m => m.Id == id);
            return View(dbDelete);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var dbDelete = context.Courses.Where(m => m.Id == id).First();
            context.Courses.Remove(dbDelete);
            context.SaveChanges();
            return RedirectToAction("Mine");
        }
        public ActionResult LectureIamgoing()
        {
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var listFollowee = context.Followings.Where(p => p.FollowerId == currentUser.Id).ToList();
            var listAttendances = context.Attendancees.Where(p => p.Attendee == currentUser.Id).ToList();
            var courses = new List<Cours>();
            foreach (var course in listAttendances)
            {
                foreach (var item in listFollowee)
                {
                    if (item.FolloweeId == course.Cours.LecturerId)
                    {
                        Cours objCourse = course.Cours;
                        objCourse.LecturerId = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(objCourse.LecturerId).Name;
                        courses.Add(objCourse);
                    }
                }
            }
            return View(courses);
        }
    }
}