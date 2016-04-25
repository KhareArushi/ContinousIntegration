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
    public class UserControllerTesting
    {
        private UserController userController;
        private int userId;

        [TestInitialize]
        public void SetUp()
        {
            userController = new UserController();
            userId = 2;
        }


        [TestMethod]
        public void Index()
        {
            var viewResult = userController.Index() as ViewResult;

            Assert.AreEqual("Login", viewResult.ViewName);
        }


        [TestMethod]
        public void Registration()
        {
            var viewResult = userController.Registration() as ViewResult;

            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public void Create()
        {
            var userRegistration = new UserRegisteration
            {
                C_FirstName = "Krutika",
                C_LastName = "Khatavkar",
                C_EmailID = "dskd@dkagdha.com",
                C_Password = "kru",
                C_CnfPassword = "kru"
            };
            var redirectToResult = userController.Create(userRegistration) as RedirectToRouteResult;

            Assert.AreEqual("SendEmail", redirectToResult.RouteValues["action"]);
            Assert.AreEqual("Account", redirectToResult.RouteValues["controller"]);
        }


        [TestMethod]
        public void List()
        {
            var mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            mockSession.Setup(s => s["LoggedUserID"]).Returns("2");
            mockControllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            userController.ControllerContext = mockControllerContext.Object;
            var viewResult = userController.List() as ViewResult;

            Assert.IsNotNull(viewResult);
        }


        [TestMethod]
        public void Login()
        {
            UserLoginModel userLoginModel = new UserLoginModel()
            {
                C_UserName = "Krutika",
                C_UserPassword = "Welcome"
            };
            var mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            mockSession.Setup(s => s["LoggedUserID"]).Returns("3"); //somevalue
            mockSession.Setup(s => s["LoggedUserName"]).Returns("Krutika");
            mockControllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            userController.ControllerContext = mockControllerContext.Object;
            var redirectToResult = userController.Login(userLoginModel) as RedirectToRouteResult;

            Assert.AreEqual("List", redirectToResult.RouteValues["action"]);
            Assert.AreEqual("User", redirectToResult.RouteValues["controller"]);
        }


        [TestMethod]
        public void GetTreeView()
        {
            var mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            mockSession.Setup(s => s["LoggedUserID"]).Returns("3"); //somevalue
            mockControllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            userController.ControllerContext = mockControllerContext.Object;
            var viewResult = userController.GetTreeView(userId) as ViewResult;

            Assert.AreEqual("GetTreeView", viewResult.ViewName);
        }
    }
}
