using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Services;
using MyFirstBlog.Models;
using System.Web.Security;
using System.Web.Script.Serialization;

namespace MyFirstBlog.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private MyBlogDBModel data = new MyBlogDBModel();
        public ActionResult Index()
        {
            ViewBag.trips=data.Trips.OrderByDescending(p=>p.date).Take(6);
            return View();
        }
        //Security
        [HttpGet]
        public void checkPassword(string username,string password)
        {

        }
        public JsonResult Login(MyFirstBlog.Models.Security.UserLogin userLogOn)
        {
            bool boolean = false;
            if (ModelState.IsValid)
            {
                string password = this.getPassword(userLogOn.UserName);
                if (string.IsNullOrEmpty(password))
                {

                }
                else
                {
                    if (password == userLogOn.Password)
                    {
                        FormsAuthentication.SetAuthCookie(userLogOn.UserName, userLogOn.RememberMe);
                        Session["current_user"] = data.users.Where(_user => _user.username == userLogOn.UserName && _user.password == userLogOn.Password).FirstOrDefault();
                        boolean=true;
                    }
                }
            }
            return Json(new { boolean = boolean }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Models.Security.loginModel userRegister)
        {
            Models.Security.Register user = userRegister.register;
            if (ModelState.IsValid)
            {
                if (!data.users.Any(_user => _user.username == user.UserName))
                {
                    if (!(data.users.Any(_user => _user.useremail == user.Email)))
                    {
                        user newuser = new user();
                        newuser.username = user.UserName;
                        newuser.password = user.Password;
                        newuser.role_id = 3;
                        newuser.useremail = user.Email;
                        newuser.userID = data.users.Count();
                        data.users.Add(newuser);
                        data.SaveChanges();
                        ViewBag.Confirm = user.UserName;
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Email is in use");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "UserName is in use");
                }
            }
            return View(userRegister);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return Redirect("/Home");
        }
        public string getPassword(string username)
        {
            using (MyBlogDBModel data = new MyBlogDBModel())
            {
                user userLogOn = data.users.Where(user => user.username == username).FirstOrDefault();
                if (userLogOn == null)
                {
                    return string.Empty;
                }
                else
                {
                    return userLogOn.password.Trim();
                }
            }
        }
    }
}
