using Microsoft.AspNetCore.Mvc;
using OnlineTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTests.Controllers
{
    public class TestController : Controller
    {
        DBContext db = new DBContext();
        Random rnd = new Random();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateTest()
        {
            if (Request.Cookies["Id"] == null)
            {
                return LocalRedirect("~/User/Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult CreateTest(Test test)
        {
            int subjectId = 0;
            if (Request.Cookies["subjectId"] != null)
            {
                subjectId = Convert.ToInt32(Request.Cookies["subjectId"]);
            }
            test.SubjectId = subjectId;
            test.CreatedDateTime = DateTime.Now;
            db.Tests.Add(test);
            db.SaveChanges();
            Response.Cookies.Append("testId", Convert.ToString(test.Id));
            return this.Preview(test.Id);
        }

        [HttpGet]
        public IActionResult Preview(int id)
        {
            Test test = db.Tests.FirstOrDefault(t => t.Id == id);
            List<TestItem> questions = db.TestItems.Where(q => q.TestId == id).ToList();
            foreach(TestItem q in questions)
            {
                q.TestItemOptions = db.TestItemOptions.Where(o => o.TestItemId == q.Id).ToList();
            }
            ViewBag.test = test;
            ViewBag.questions = questions;
            Subject subject = db.Subjects.FirstOrDefault(s => s.Id == test.SubjectId);
            int currentUserId = 0;
            if (Request.Cookies["Id"] != null)
            {
                currentUserId = Convert.ToInt32(Request.Cookies["Id"]);
            }
            if(currentUserId == subject.TeacherId)
            {
                return View("~/Views/Test/Preview.cshtml");
            }
            else
            {
                return View("~/Views/Test/TakeTest.cshtml");
            }
        }

        [HttpGet]
        public IActionResult AddQuestion()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddQuestion(TestItem testItem)
        {
            int testId = 0;
            if (Request.Cookies["testId"] != null)
            {
                testId = Convert.ToInt32(Request.Cookies["testId"]);
            }
            testItem.TestId = testId;
            db.TestItems.Add(testItem);
            db.SaveChanges();
            Response.Cookies.Append("qId", Convert.ToString(testItem.Id));
            return this.PreviewQuestion(testItem.Id);
        }

        [HttpGet]
        public IActionResult PreviewQuestion(int qId)
        {
            TestItem testItem = db.TestItems.FirstOrDefault(t => t.Id == qId);
            List<TestItemOption> options = db.TestItemOptions.Where(o => o.TestItemId == qId).ToList();
            int testId = 0;
            if (Request.Cookies["testId"] != null)
            {
                testId = Convert.ToInt32(Request.Cookies["testId"]);
            }
            ViewBag.testItem = testItem;
            ViewBag.options = options;
            ViewBag.testId = testId;
            return View("~/Views/Test/PreviewQuestion.cshtml");
        }

        [HttpGet]
        public IActionResult AddOption()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddOption(TestItemOption testItemOption)
        {
            int qId = 0;
            if (Request.Cookies["qId"] != null)
            {
                qId = Convert.ToInt32(Request.Cookies["qId"]);
            }
            testItemOption.TestItemId = qId;
            db.TestItemOptions.Add(testItemOption);
            db.SaveChanges();
            return PreviewQuestion(qId);
        }

        [HttpPost]
        public IActionResult SubmitTest()
        {
            int testId = 0;
            if (Request.Cookies["testId"] != null)
            {
                testId = Convert.ToInt32(Request.Cookies["testId"]);
            }
            int currentUserId = 0;
            if (Request.Cookies["Id"] != null)
            {
                currentUserId = Convert.ToInt32(Request.Cookies["Id"]);
            }
            int result = rnd.Next(0, 101);
            TestSummary testSummary = new TestSummary
            {
                OwnerUserId = currentUserId,
                TestId = testId,
                Points = result,
                CreatedDateTime = DateTime.Now
            };
            db.TestsResults.Add(testSummary);
            db.SaveChanges();
            return RedirectToAction("Message", new { s = "Your result: " + result + "!" });
        }

        public ActionResult Message(string s)
        {
            ViewBag.message = s;
            return View();
        }
    }
}
