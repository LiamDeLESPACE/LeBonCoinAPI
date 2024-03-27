using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeBonCoinAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeBonCoinAPI.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.Controllers.Tests
{
    [TestClass()]
    public class AdminsControllerTests
    {
        DataContext _context;
        private AdminsController _controller;
        List<Admin> _testList;

        [TestInitialize]
        public void InitialisationDesTests()
        {
        
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=localhost; port=5432; Database=LeBonCoinSAE; uid=postgres; password=postgres;");
            _context = new DataContext();
            _controller = new AdminsController(_context);
            _testList = GetTestAdmins();
        }

        [TestMethod()]
        public void AdminsControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAdminsTest_RecupereTOUSLesAdmins()
        {
            //Act
            var result = _controller.GetAdmins().Result.Value;
            //Assert
            Assert.AreEqual(_testList.Count, result.Count());
        }

        public void GetAdminsTest_RecupereLeBonType()
        {
            //Act
            var result = _controller.GetAdmins().Result.Value;
            //Assert
            CollectionAssert.AllItemsAreInstancesOfType((System.Collections.ICollection)result, typeof(Admin));
        }

        [TestMethod()]
        public void GetAdminTest_RecupereLadmin()
        {
            //Act
            var result = _controller.GetAdmin(3);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testList[3].Email, result.Result.Value.Email);
        }

        [TestMethod()]
        public void GetAdminTest_NeRecuperepasLadmin()
        {
            //Act
            var result = _controller.GetAdmin(999);
            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod()]
        public void PutAdminTest_CreationOK()
        {
            //Arrange
            Admin adm = new Admin { AdresseId = 1, HashMotDePasse = "$2b$12$6PLiq9Mf3CgnjA5Nh6S2xuJ/IV.2lLbQVzLRF0k68imVl5bq7rlWe", Telephone = "0710778326", Service = "PetiteAnnonce", Email = "Debisse.Paul@lebonendroit.com" };
            //Act
            var result = _controller.PutAdmin(5,adm).Result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Admin>), "Pas un ActionResult<Admin>");
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Admin), "Pas une série");
            adm.ProfilId = ((Admin)actionResult.Value).ProfilId;
            Assert.AreEqual(adm, (Admin)actionResult.Value, "Series pas identiques");
        }

        [TestMethod()]
        public void PutAdminTest_ServiceMissing_CreationFailed()
        {
            //Arrange
            Admin adm = new Admin { AdresseId = 1, HashMotDePasse = "$2b$12$6PLiq9Mf3CgnjA5Nh6S2xuJ/IV.2lLbQVzLRF0k68imVl5bq7rlWe", Telephone = "0710778326", Email = "Debisse.Paul@lebonendroit.com" };
            //Act
            var result = _controller.PostAdmin(adm).Result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Admin>), "Pas un ActionResult<Admin>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Admin), "Pas une série");
            adm.ProfilId = ((Admin)actionResult.Value).ProfilId;
            Assert.AreEqual(adm, (Admin)actionResult.Value, "Series pas identiques");
        }

        [TestMethod()]
        public void PostAdminTest_CreationOK()
        {
            //Arrange
            Admin adm = new Admin { AdresseId = 1, HashMotDePasse = "$2b$12$6PLiq9Mf3CgnjA5Nh6S2xuJ/IV.2lLbQVzLRF0k68imVl5bq7rlWe", Telephone = "0710778326", Service = "PetiteAnnonce", Email = "Debisse.Paul@lebonendroit.com" };
            //Act
            var result = _controller.PostAdmin(adm).Result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Admin>), "Pas un ActionResult<Admin>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Admin), "Pas une série");
            adm.ProfilId = ((Admin)actionResult.Value).ProfilId;
            Assert.AreEqual(adm, (Admin)actionResult.Value, "Series pas identiques");
        }

        [TestMethod()]
        public void PostAdminTest_EmailMissing_CreationFailed()
        {
            //Arrange
            Admin adm = new Admin { AdresseId = 1, HashMotDePasse = "$2b$12$6PLiq9Mf3CgnjA5Nh6S2xuJ/IV.2lLbQVzLRF0k68imVl5bq7rlWe", Telephone = "0710778326", Service = "PetiteAnnonce"};
            //Act
            var result = _controller.PostAdmin(adm).Result;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Admin>), "Pas un ActionResult<Admin>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Admin), "Pas une série");
            adm.ProfilId = ((Admin)actionResult.Value).ProfilId;
            Assert.AreEqual(adm, (Admin)actionResult.Value, "Series pas identiques");
        }

        [TestMethod()]
        public void DeleteAdminTest()
        {
            Assert.Fail();
        }

        private List<Admin> GetTestAdmins()
        {
            var testAdmins = new List<Admin>();
            testAdmins.Add(new Admin { AdresseId = 1, HashMotDePasse = "$2b$12$6PLiq9Mf3CgnjA5Nh6S2xuJ/IV.2lLbQVzLRF0k68imVl5bq7rlWe", Telephone = "0710778326", Service = "PetiteAnnonce", Email = "Debisse.Paul@lebonendroit.com" });
            testAdmins.Add(new Admin { AdresseId = 2, HashMotDePasse = "$2b$12$X6UO8I8XC6wtHRMKdywFBuYzrV/08B9n4y9XmdTeQjwwuG.3BPKDW", Telephone = "0742416738", Service = "PetiteAnnonce", Email = "Menvusa.Gerard@lebonendroit.com" });
            testAdmins.Add(new Admin { AdresseId = 3, HashMotDePasse = "$2b$12$GTT6PGIzAfqnZrPn0Ek4i.NQth2.UU9J6b6vrvLJGBaDEmTZDhtJG", Telephone = "0736615553", Service = "PetiteAnnonce", Email = "Tard.Gui@lebonendroit.com" });
            testAdmins.Add(new Admin { AdresseId = 4, HashMotDePasse = "$2b$12$IPF3t30AKjwhPzY44dJZ3Oe1pGfAbpkpG7qA1InKqA/f1gKE3GnsO", Telephone = "0732639420", Service = "Informatique", Email = "Paul.Jean@lebonendroit.com" });

            return testAdmins;
        }
    }

}