using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using MyFirstBlog.Models;
using System.Web.Mvc;
namespace MyFirstBlog.Common
{
    public class CustomPrincipal : IPrincipal
    {
        private MyBlogDBModel data = new MyBlogDBModel();
        private user Account;
        public CustomPrincipal(user Account)
        {
            this.Account = Account;
            this.Identity = new GenericIdentity(Account.username);
        }
        public IIdentity Identity
        {
            get;set;
        }

        public bool IsInRole(string role)
        {
            var roles = role.Split(new char[] { ',' });
            string userRole = data.userRoles.Where(p => p.roleID == this.Account.role_id).FirstOrDefault().role.Trim();
            return roles.Any(r => r==userRole);
        }
    }
}