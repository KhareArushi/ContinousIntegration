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
    public class RolesControllerTesting
    {
        private RolesController rolesController;
        private int userId;

        [TestInitialize]
        public void SetUp()
        {
            rolesController = new RolesController();
            userId = 2;
        }

        [TestMethod]
        public void Index()
        {

            var viewResult = rolesController.Index() as ViewResult;
            var configurationModel = (ConfigurationModel)viewResult.Model;

            Assert.IsNotNull(configurationModel.Users);
            Assert.IsNotNull(configurationModel.Roles);
            Assert.IsNotNull(configurationModel.AssignedProjects);
            Assert.IsNotNull(configurationModel.AvailableProjects);
        }

        [TestMethod]
        public void CheckingIndexAction()
        {
            var viewResult = rolesController.Index() as ViewResult;

            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public void AccessRoles()
        {
            ConfigurationModel configurationDetails = new ConfigurationModel();
            configurationDetails.SelectedUserID = userId;
            configurationDetails.SelectedRoleID = 1;
            var isSuccess = rolesController.AccessRoles(configurationDetails) as string;

            Assert.AreEqual("true", isSuccess);
        }

        [TestMethod]
        public void GetConfigurationDetails()
        {
            var jsonResult = rolesController.GetConfigurationDetails(userId) as JsonResult;
            var configurationModel = (ConfigurationModel)jsonResult.Data;

            Assert.IsNotNull(configurationModel.Users);
            Assert.IsNotNull(configurationModel.Roles);
            Assert.IsNotNull(configurationModel.RoleId);
            Assert.IsNotNull(configurationModel.AssignedProjects);
            Assert.IsNotNull(configurationModel.AvailableProjects);
        }
    }
}
