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
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
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
                MailMessage EmailText = new MailMessage(Email, "rasty.home@gmail.com"); //from to emails
                EmailText.Subject = "Your new Password";
                EmailText.Body = $"Hello {UserName},<br/> your password was reseted to:{tempPassword} <br/> After Login to your account you will be able to change this password to new one <br/>,best regards <br/>Development team";
                EmailText.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(EmailText);
                ViewBag.Succes = "The message has been sent";
            }
            else
            {
                ViewBag.Succes = "The information provided does not match our records";
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
                    ViewBag.Succes = "Your password was changed";
                    Membership.GetUser().ChangePassword(Password, NewPassword);
                }
                else
                {
                    ViewBag.Succes = "Wrong old password";
                }
                
            }
            else
            {
                ViewBag.Succes = "Please Check your input data";
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Registration Error");
                }
            }


            return View(model);
        }

        
        



    }



}
