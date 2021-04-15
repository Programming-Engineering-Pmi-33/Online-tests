using Microsoft.AspNetCore.Mvc;
using OnlineTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace OnlineTests.Controllers
{
    public class UserController : Controller
    {
        DBContext db = new DBContext();

        public ActionResult Message(string s)
        {
            ViewBag.message = s;
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(BaseUser user)
        {
            user.Password = Crypto.HashPassword(user.Password);
            user.PasswordConfirm = user.Password;
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Message", new { s = "Welcome, " + user.FirstName + "!"});

        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(BaseUser model)
        {
            BaseUser user = null;

            using (DBContext context = new DBContext())
            {
                string email = model.EmailAddress;
                user = context.Users.FirstOrDefault(u => u.EmailAddress == email);

            }
            if (user != null)
            {
                if (!Crypto.VerifyHashedPassword(user.Password, model.Password))
                {
                    return RedirectToAction("Message", new { s = "Wrong password" });
                }
                Response.Cookies.Append("Id", Convert.ToString(user.Id));
                return RedirectToAction("Message", new { s = "Welcome!" });
            }
            else
            {

                return RedirectToAction("Message", new { s = "No user with this username and password" });
            }
        }

        public IActionResult LogOut()
        {
            Response.Cookies.Delete("Id");
            return RedirectToAction("Message", new { s = "Success" });
        }

        public string GetInfo()
        {

            string res;
            string CurUserId = Request.Cookies["Id"];
            if (CurUserId == null)
            {
                res = "You are not logged in";
            }
            else
            {
                string curId = Convert.ToString(CurUserId);
                res = "Current user id is " + curId;
            }
            return res;
        }

        public IActionResult Profile(int Id)
        {
            BaseUser user = db.Users.FirstOrDefault(u => u.Id == Id);
            if (user == null)
            {
                return RedirectToAction("Message", new { s = "Not found" });
            }
            ViewBag.user = user;
            ViewBag.books = null;
            ViewBag.allow = false;
            if (Request.Cookies["Id"] != null)
            {
                int curId = Convert.ToInt32(Request.Cookies["Id"]);
                if (Id == curId) ViewBag.allow = true;
            }

            return View();
        }

        public IActionResult Me()
        {
            if (Request.Cookies["Id"] == null)
            {
                return LocalRedirect("~/User/Login");
            }
            int Id = Convert.ToInt32(Request.Cookies["Id"]);
            return RedirectToAction("Profile", new { Id = Id });
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            int curId = Convert.ToInt32(Request.Cookies["Id"]);
            if (Id != curId)
            {
                return RedirectToAction("Message", new { s = "Not allowed" });
            }
            BaseUser user = db.Users.FirstOrDefault(u => u.Id == Id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(BaseUser form)
        {
            BaseUser user = db.Users.FirstOrDefault(u => u.Id == form.Id);
            user.EmailAddress = form.EmailAddress;
            user.FirstName = form.FirstName;
            user.LastName = form.LastName;
            db.SaveChanges();
            return RedirectToAction("Profile", new { Id = user.Id });
        }
    }
}

