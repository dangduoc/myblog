using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace MyFirstBlog.Models.Security
{
    public class loginModel
    {
        public UserLogin userLogin { get; set; }
        public Register register { get; set; }
    }
    public class UserLogin
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "Mật khẩu có từ 6 đến 18 ký tự")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class Register
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(18, ErrorMessage = "Mật khẩu có từ 6 đến 18 ký tự", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Nhập lại chưa khớp")]
        public string ConfirmPassword { get; set; }
    }
}