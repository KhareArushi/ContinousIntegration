using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using ContinousIntegration.Models;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.IO;
using System.Globalization;

namespace ContinousIntegration.Controllers
{
    /// <summary>
    /// Controller class to manage accounts
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// This method will send an email to notify the user
        /// of his registration
        /// </summary>
        /// <param name="reg">URegistration model</param>
        /// <returns>Email sent view</returns>
        [HttpGet]
        public ActionResult SendEmail(URegisteration reg)
        {           
            var message = EMailTemplate("WelcomeEmail");
            message = message.Replace("@ViewBag.Name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reg.C_FirstName));

            //Sends email
            MessageServices.SendEmail(reg.C_EmailID, "Welcome to Continuous Integration!", message);
            return View("EmailSent");
        }
       
        /// <summary>
        /// This will return confirmation of email sent
        /// </summary>
        /// <returns>View of email sent</returns>
        [HttpGet]
        public ActionResult EmailSent()
        {
            return View();
        }
       
        /// <summary>
        /// This method will be called whilst user is logging in
        /// </summary>
        /// <param name="returnUrl">String parameter of url</param>
        /// <returns>Login view</returns>
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }      

          public static string EMailTemplate(string template)
        {
            var templateFilePath = HostingEnvironment.MapPath("~/Views/templates/") + template + ".cshtml";
            StreamReader objstreamreaderfile = new StreamReader(templateFilePath); 
            var body = objstreamreaderfile.ReadToEnd();
            objstreamreaderfile.Close();
            return body;
        }
            
        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion



    }
}
