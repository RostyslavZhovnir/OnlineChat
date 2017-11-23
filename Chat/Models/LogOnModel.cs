using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chat.Models
{
    public class LogOnModel
    {
        [Required(ErrorMessage = "Please enter User Name")]
        [Display(Name = "User Name (Login)")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Please enter User Name")]
        [Display(Name = "User Name (Login)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter E-mail")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords does not match")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgetPassword {

        [Required(ErrorMessage = "Please enter User Name")]
        [Display(Name = "User Name (Login)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter E-mail")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

       

    }
    public class ChangePassword {

        [Required(ErrorMessage = "Please enter old or temporary password")]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter new password")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [StringLength(1000, MinimumLength = 6, ErrorMessage = "Password has to be longer than 6 characters")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Passwords does not match")]
        public string ConfirmPassword { get; set; }

    }
}