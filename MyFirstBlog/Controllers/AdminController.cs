using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstBlog.Models;
using System.Web.Security;
namespace MyFirstBlog.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        private MyBlogDBModel data = new MyBlogDBModel();
        [MyFirstBlog.Common.CustomAthorize(Roles = "superadmin,admin")]
        public ActionResult Index()
        {
            return View();
        }
        [MyFirstBlog.Common.CustomAthorize(Roles = "superadmin,admin")]
        public ActionResult DailyPost()
        {
            return View(data.DailyPosts);
        }
        //Trip----------------------------------
        //
        [MyFirstBlog.Common.CustomAthorize(Roles = "superadmin,admin,friends")]
        [HttpPost]
        public ActionResult Trips()
        {
            user crnt_user = (user)Session["current_user"];
            if (crnt_user.role_id <= 1)
            {
                return View(data.Trips);
            }
            else 
            {
                return View(data.Trips.Where(t => t.userID == crnt_user.userID));
            } 
        }
        
        [MyFirstBlog.Common.CustomAthorize(Roles = "superadmin,admin,friends")]
        [HttpPost]
        public void DeleteTrip(int id)
        {
            var tripid = Convert.ToInt32(id);
            var trip = data.Trips.Where(t => t.postID == tripid).FirstOrDefault();
            data.Trips.Remove(trip);
            data.SaveChanges();
        }
        [MyFirstBlog.Common.CustomAthorize(Roles = "superadmin,admin")]
        [HttpPost]
        public ActionResult PendingTrips()
        {
            return View(data.previewTrips);
        }
        [MyFirstBlog.Common.CustomAthorize(Roles = "superadmin,admin")]
        [HttpPost]
        public void DeletePendingTrip(string id)
        {
            var postid = Convert.ToInt32(id);
            var trip = data.previewTrips.Where(t => t.previewID == postid).FirstOrDefault();
            data.previewTrips.Remove(trip);
            data.SaveChanges();
        }
        //User------
        //
        [MyFirstBlog.Common.CustomAthorize(Roles = "superadmin,admin")]
        [HttpPost]
        public ActionResult Users()
        {
            return View(data.users);
        }
        public JsonResult UserDetail(int id)
        {
            var u = data.userInfoes.Where(user => user.userID == id).FirstOrDefault();
            var role = data.userRoles.Where(user => user.roleID == data.users.Where(p => p.userID == id).FirstOrDefault().role_id).FirstOrDefault().role;
            return Json(new {
                ten = u.name,
                gioitinh = u.gender,
                diachi = u.addressLine,
                ngaytao = u.dateCreate.Value.ToShortDateString(),
                vaitro=role
            }
            , JsonRequestBehavior.AllowGet);
        }
        //Login------------
        //
        [AllowAnonymous]
        public ActionResult Login()
        {
            if(Session["current_user"] != null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
            }
            return View(new MyFirstBlog.Models.Security.UserLogin());
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(MyFirstBlog.Models.Security.UserLogin userLogOn,string returnUrl)
        {
            string inform = "";
            if (ModelState.IsValid)
            {
                string password = this.getPassword(userLogOn.UserName);
                if (string.IsNullOrEmpty(password))
                {
                    inform = "Username does not exsist";
                }
                else
                {
                    if (password == userLogOn.Password)
                    {
                        var user = data.users.Where(_user => _user.username == userLogOn.UserName && _user.password == userLogOn.Password).FirstOrDefault();
                        if (user.role_id <= 1)
                        {
                            FormsAuthentication.SetAuthCookie(userLogOn.UserName, userLogOn.RememberMe);
                            Session["current_user"] = data.users.Where(_user => _user.username == userLogOn.UserName && _user.password == userLogOn.Password).FirstOrDefault();
                            return RedirectToLocal(returnUrl);
                        }
                        else
                        {
                            inform = "You don't have permission";
                        }
                    }
                    else
                    {
                        inform = "Password is incorrect";
                    }
                }
            }
            else
                inform = "input is invalid";
            ViewBag.inform = inform;
            return View();
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
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
