﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeBonCoinAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeBonCoinAPI.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Azure;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;

namespace LeBonCoinAPI.Controllers.Tests
{
    [TestClass()]
    public class AdminsControllerTests
    {
        private AdminsController _controller;

        private Mock<DataContext> _context;

        //Arrange
        Admin admin;
        List<Admin> testListe;

        public AdminsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=localhost; port=5432; Database=LeBonCoinSAE; uid=postgres; password=postgres;");
            _context = new Mock<DataContext>();
            _controller = new AdminsController(_context.Object);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            admin = new Admin { ProfilId = 1, HashMotDePasse = "$2b$12$6PLiq9Mf3CgnjA5Nh6S2xuJ/IV.2lLbQVzLRF0k68imVl5bq7rlWe", Telephone = "0710778326", Service = "PetiteAnnonce", Email = "Debisse.Paul@lebonendroit.com" };

            testListe = new List<Admin>();
            testListe.Add(new Admin { ProfilId = 1, HashMotDePasse = "$2b$12$6PLiq9Mf3CgnjA5Nh6S2xuJ/IV.2lLbQVzLRF0k68imVl5bq7rlWe", Telephone = "0710778326", Service = "PetiteAnnonce", Email= "Debisse.Paul@lebonendroit.com" });
            testListe.Add(new Admin { ProfilId = 2, HashMotDePasse = "$2b$12$X6UO8I8XC6wtHRMKdywFBuYzrV/08B9n4y9XmdTeQjwwuG.3BPKDW", Telephone = "0742416738", Service = "PetiteAnnonce", Email = "Menvusa.Gerard@lebonendroit.com" });

        }

        [TestMethod()]
        public void GetAdmin_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var result = _controller.GetAdmin(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Admin>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Admin>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Admin), "Pas une Admin");
            Assert.AreEqual(admin, (Admin)actionResult.Value, "Admin pas identiques");
        }

        [TestMethod()]
        public void GetAdmin_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var result = _controller.GetAdmin(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Admin>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Admin pas null");
        }

        [TestMethod()]
        public void GetAdmins_ReturnsRightItems()
        {

            //Act
            var result = _controller.GetAdmins();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Admin>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Admin>> actionResult = result.Result as ActionResult<IEnumerable<Admin>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.ProfilId <= 2).ToList(), "Pas les mêmes Admins");
        }

        [TestMethod()]
        public void PostAdmin_ModelValidated_CreationOK()
        {

            //Act
            var result = _controller.PostAdmin(admin).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Admin>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Admin), "Pas une Admin");
            admin.Email = ((Admin)actionResult.Value).Email;
            Assert.AreEqual(admin, (Admin)actionResult.Value, "Admins pas identiques");

        }
        [TestMethod()]
        public void PostAdmin_CodeInsee_CreationFailed()
        {

            //Act
            var result = _controller.PostAdmin(admin).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Admin), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            int id = 2;//Mauvais ID

            // Act
            var result = await _controller.PutAdmin(id, admin);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {

            int id = 1; //BonID

            // Act
            var result = await _controller.PutAdmin(id, admin);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteAdminTest()
        {

            //Act
            var result = _controller.GetAdmin(1);

            //Existence
            var actionResult = result.Result as ActionResult<Admin>;
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Admin), "Pas une Admin");

            //Act
            var resultDest = _controller.DeleteAdmin(1);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<Admin>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "Admin pas null");
        }
    }
}