using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyFirstBlog.Models;
namespace MyFirstBlog.Common
{
    public class Methods
    {
        private static MyBlogDBModel data = new MyBlogDBModel();
        public static string findusernameByID(int id)
        {
            return data.users.Where(u => u.userID == id).FirstOrDefault().username;
        }
    }
}