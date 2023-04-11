using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace BigSchool.Controllers
{
    public class AttendancesController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Attend(Cours attendanceDto)
        {
            var userID = User.Identity.GetUserId();
            BigSchoolContext context = new BigSchoolContext();
            if (context.Attendancees.Any(p => p.Attendee == userID && p.CourseId == attendanceDto.Id))
            {
                //return BadRequest("The attendance already exists!");
                context.Attendancees.Remove(context.Attendancees.SingleOrDefault(p => p.Attendee == userID && p.CourseId == attendanceDto.Id));
                context.SaveChanges();
                return Ok("cancel");
            }
            var attendance = new Attendancee(){
                CourseId = attendanceDto.Id,
                Attendee = User.Identity.GetUserId()
            };
            context.Attendancees.Add(attendance);
            context.SaveChanges();
            return Ok();
        }
    }
}
