using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstBlog.Models;
using PagedList;

namespace MyFirstBlog.Controllers
{
    public class FriendsandMeController : Controller
    {
        //
        // GET: /FriendsandMe/
        private MyBlogDBModel data = new MyBlogDBModel();
        public ActionResult Trips()
        {
            ViewBag.lastests = data.Trips.OrderByDescending(p => p.date).Take(4);
            return View(data.Trips.OrderByDescending(p => p.date));
        }      
        public ActionResult Trip(int id)
        {
            var trip = data.Trips.Where(t => t.postID == id).FirstOrDefault();
            ViewBag.others = data.Trips.OrderByDescending(p=>p.date).Take(5);
            return View(trip);
        }
        public ActionResult PreviewTrip(int id)
        {
            var trip = data.previewTrips.Where(t => t.previewID == id).FirstOrDefault();
            return View(trip);
        }
       [MyFirstBlog.Common.CustomAthorize(Roles = "superadmin,admin,friends")]
        [HttpPost]
        public ActionResult WriteTrip()
        {
            return View();
        }
        [MyFirstBlog.Common.CustomAthorize(Roles = "superadmin,admin,friends")]
       [HttpPost,ValidateInput(false)]
       public ActionResult WriteTriped(FormCollection form)
        {
            if (!(String.IsNullOrEmpty(form["title"])) && !(String.IsNullOrEmpty(form["content"])) && (!String.IsNullOrEmpty(form["summary"])) && (!String.IsNullOrEmpty(form["thumbnail"])))
            {
                previewTrip newtrip = new previewTrip();
                var total=data.previewTrips.Count();
                if (total == 0)
                {
                    newtrip.previewID = 0;
                }
                else
                {
                    newtrip.previewID = data.previewTrips.OrderByDescending(p => p.previewID).FirstOrDefault().previewID + 1;
                }
                newtrip.postID = data.Trips.OrderByDescending(p => p.postID).FirstOrDefault().postID + 1;
                newtrip.date = DateTime.Now;
                newtrip.userID = ((user)Session["current_user"]).userID;
                newtrip.postTitle = form["title"];
                newtrip.postContent = form["content"];
                newtrip.postSummary = form["summary"];
                newtrip.imgThumbnail = form["thumbnail"];
                if (!String.IsNullOrEmpty(form["folder"]))
                {
                    newtrip.imageFolder = form["folder"];
                }
                data.previewTrips.Add(newtrip);
                data.SaveChanges();
                return Redirect("http://dangduoc.azurewebsites.net/FriendsandMe/PreviewTrip?id=" + newtrip.previewID);
            }
            else
                return View("Error");
        }
        [MyFirstBlog.Common.CustomAthorize(Roles = "superadmin,admin,friends")]
        [HttpGet]
        public ActionResult EditTrip(int id)
        {
            user crnt_user = (user)Session["current_user"];
            var trip = data.Trips.Where(t => t.postID == id).FirstOrDefault();
            if (crnt_user.userID != trip.userID)
                return Content("Bạn không thể sửa bài của người khác");
            else return View(trip);
        }
        public ActionResult Moment(int id)
        {
            return View(data.BestMoments.Where(p => p.momentID == id).FirstOrDefault());
        }
        public ActionResult Moments()
        {
            var moments=data.BestMoments.OrderByDescending(p => p.Date_create).ToList<BestMoment>();
            List<List<BestMoment>> list = new List<List<BestMoment>>();
            for (int i = 0; i < moments.Count; i=i+3)
            {
                int j = i;
                List<BestMoment> tmp = new List<BestMoment>();
                while (((j - i) <= 2)&&(j<moments.Count))
                {
                    tmp.Add(moments[j]);
                    j++;
                }
                list.Add(tmp);
            }
            ViewBag.list = list;
            return View();

        }
        //-------------------------------------------
        [MyFirstBlog.Common.CustomAthorize(Roles="friends,admin,superadmin")]
        [HttpPost]
        public void Post(BestMoment newmoment)
        {
                var crnt_user = (MyFirstBlog.Models.user)Session["current_user"];
                var newpost = new BestMoment();
                newpost.Date_create = DateTime.Now;
                newpost.momentID = data.BestMoments.OrderByDescending(p=>p.momentID).FirstOrDefault().momentID+1;
                newpost.Title = newmoment.Title;
                newpost.Image = newmoment.Image;
                newpost.Summary = newmoment.Summary;
                newpost.userID=crnt_user.userID;
                data.BestMoments.Add(newpost);
                data.SaveChanges();
        }
    }
}
