using Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Chat.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Topics");

                    }
                }
                else
                {
                    ViewBag.Succes = "Wrong Login or Password";
                 
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Topics");
        }


        public ActionResult ResendPassword()
        {
            return View();

        }

        [HttpPost]
        public ActionResult ResendPassword(string UserName, string Email)
        {
            if (Membership.GetUser(UserName) != null && Membership.GetUserNameByEmail(Email.ToLower())!=null)
            {
                //string psw = Membership.GetUserNameByEmail(model.Email);
                //string password = "pass@word";
                MembershipUser mu = Membership.GetUser(UserName);
                string tempPassword = mu.ResetPassword();
                //mu.ChangePassword(mu.ResetPassword(), password);
                MailMessage EmailText = new MailMessage("info@clickchats.com",Email ); //from to emails
                EmailText.Subject = "Your password was reset www.clickchats.com";
                EmailText.Body = $"Hello {UserName},<br/>Your temporary password has been set to:{tempPassword} <br/> After Login, you will be able to change it  <br/>,best regards <br/>ClickChats.com";
                EmailText.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(EmailText);
                ViewBag.Succes = "Your temporary password has been sent";
            }
            else
            {
                ViewBag.Succes = "inserted information was wrong";
            }
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();

        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePassword model, string Password,string NewPassword)
        {
            if (ModelState.IsValid)
            {
               bool x= Membership.GetUser().ChangePassword(Password, NewPassword);
                if (x)
                {
                    ViewBag.Succes = "password has been changed";
                    Membership.GetUser().ChangePassword(Password, NewPassword);
                }
                else
                {
                    ViewBag.Succes = "wrong password";
                }
                
            }
            else
            {
                ViewBag.Succes = "inserted information was wrong ";
            }


            return View();

        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email.ToLower(),
                    passwordQuestion: null, passwordAnswer: null, isApproved: true,
                    providerUserKey: null, status: out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Topics");
                }
                else
                {
                    ViewBag.Succes= "username already exists";
                }
            }


            return View(model);
        }

        
        



    }



}
