using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chat.Models
{
    public class LogOnModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        [Display(Name = "Имя")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль пользователя")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        [Display(Name = "Имя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите e-mail")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль пользователя")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgetPassword {

        [Required(ErrorMessage = "Введите имя пользователя")]
        [Display(Name = "Имя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите e-mail")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

       

    }
    public class ChangePassword {

        [Required(ErrorMessage = "Введите cтарый или временный пароль пользователя")]
        [DataType(DataType.Password)]
        [Display(Name = "Старый пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите новый пароль пользователя")]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        [StringLength(1000, MinimumLength = 6, ErrorMessage = "Пароль должен состоять хотя бы из 6 символов")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

    }
}