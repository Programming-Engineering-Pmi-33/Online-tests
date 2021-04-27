using Microsoft.AspNetCore.Mvc;
using OnlineTests.BLL;
using OnlineTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTests.Controllers
{
    public class SubjectController : Controller
    {
        DBContext db = new DBContext();
        SubjectHelper subjectHelper = new SubjectHelper();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateSubject()
        {
            if (Request.Cookies["Id"] == null)
            {
                return LocalRedirect("~/User/Login");
            }
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateSubject(Subject subject)
        {
            string CurUserId = Request.Cookies["Id"];
            subject.TeacherId = Convert.ToInt32(CurUserId);
            db.Subjects.Add(subject);
            db.SaveChanges();
            return Subject(subject.Id);
        }


        [HttpGet]
        public IActionResult Subject(int id)
        {
            Subject subject = db.Subjects.FirstOrDefault(u => u.Id == id);
            BaseUser teacher = db.Users.FirstOrDefault(u => u.Id == subject.TeacherId);

            Response.Cookies.Append("subjectId", Convert.ToString(id));

            ViewBag.subject = subject;
            ViewBag.teacherName = teacher.FirstName + " " + teacher.LastName;
            

            int currentUserId = 0;
            if(Request.Cookies["Id"] != null)
            {
                currentUserId = Convert.ToInt32(Request.Cookies["Id"]);
            }
            if(subject.TeacherId == currentUserId)
            {
                List<SubjectSummaryModel> subjectSummary = subjectHelper.GetSummaryForSubject(id);
                ViewBag.subjectSummary = subjectSummary;
                return View("~/Views/Subject/TeacherSubject.cshtml");
            }
            else
            {
                List<StudentSubjectSummary> subjectSummary = subjectHelper.GetSummaryForStudentAndSubject(currentUserId, id);
                ViewBag.subjectSummary = subjectSummary;
                return View("~/Views/Subject/StudentSubject.cshtml");
            }
            
        }

        [HttpGet]
        public IActionResult MyLearnings()
        {
            int currentUserId = 0;
            if (Request.Cookies["Id"] != null)
            {
                currentUserId = Convert.ToInt32(Request.Cookies["Id"]);
            }
            ViewBag.learnings = subjectHelper.GetLearningSummary(currentUserId);
            return View();
        }


        [HttpGet]
        public IActionResult MyCourses()
        {
            int currentUserId = 0;
            if (Request.Cookies["Id"] != null)
            {
                currentUserId = Convert.ToInt32(Request.Cookies["Id"]);
            }
            ViewBag.courses = db.Subjects.Where(s => s.TeacherId == currentUserId);
            return View();
        }
    }
}
