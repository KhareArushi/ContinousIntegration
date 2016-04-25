using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContinousIntegration.Controllers;
using System.Web.Mvc;
using ContinousIntegration.Models;
using System.Collections.Generic;
using ContinousIntegration;
using Moq;

namespace ContinuousIntegrationTest
{
    [TestClass]
    public class AccountControllerTesting
    {
        private AccountController accountController;

        [TestInitialize]
        public void SetUp()
        {
            accountController = new AccountController();
        }

        [TestMethod]
        public void SendEmail()
        {
            UserRegisteration userRegistrations = new UserRegisteration()
            {
                C_FirstName = "Krutika",
                C_EmailID = "krutikakhatavkar123@gmail.com"
            };
            var viewResult = accountController.SendEmail(userRegistrations) as ViewResult;

            Assert.AreEqual("EmailSent", viewResult.ViewName);
        }

        [TestMethod]
        public void EmailSent()
        {
            var viewResult = accountController.EmailSent() as ViewResult;

            Assert.AreEqual("EmailSent", viewResult.ViewName);
        }

        [TestMethod]
        public void EmailTemplate()
        {
            var welcomeMsg = AccountController.EmailTemplate("WelcomeEmail") as string;

            Assert.IsNotNull(welcomeMsg);
        }

        [TestMethod]
        public void Login()
        {
            var result = accountController.Login("User/List") as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
