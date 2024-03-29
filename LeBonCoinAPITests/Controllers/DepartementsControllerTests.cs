using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeBonCoinAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeBonCoinAPI.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Castle.Components.DictionaryAdapter.Xml;
using Moq;

namespace LeBonCoinAPI.Controllers.Tests
{
    [TestClass()]
    public class DepartementsControllerTests
    {
        private DepartementsController _controller;

        private Mock<DataContext> _context;

        //Arrange
        Departement Departement;
        List<Departement> testListe;

        public DepartementsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=localhost; port=5432; Database=LeBonCoinSAE; uid=postgres; password=postgres;");
            _context = new Mock<DataContext>();
            _controller = new DepartementsController(_context.Object);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            Departement = new Departement { DepartementCode = "1", Nom = "Ain" };

            testListe = new List<Departement>();
            testListe.Add(new Departement { DepartementCode = "1", Nom = "Ain" });
            testListe.Add(new Departement { DepartementCode = "2", Nom = "Aisne" });


        }

        [TestMethod()]
        public void GetDepartement_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var result = _controller.GetDepartement("1");

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Departement>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Departement>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Departement), "Pas une Departement");
            Assert.AreEqual(Departement, (Departement)actionResult.Value, "Departement pas identiques");
        }

        [TestMethod()]
        public void GetDepartement_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var result = _controller.GetDepartement("0");

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Departement>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Departement pas null");
        }

        [TestMethod()]
        public void GetDepartements_ReturnsRightItems()
        {

            //Act
            var result = _controller.GetDepartements();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Departement>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Departement>> actionResult = result.Result as ActionResult<IEnumerable<Departement>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsTrue(testListe.Any(d => d.DepartementCode == "1"));
            Assert.IsTrue(testListe.Any(d => d.DepartementCode == "2"));
        }

        [TestMethod()]
        public void PostDepartement_ModelValidated_CreationOK()
        {

            //Act
            var result = _controller.PostDepartement(Departement).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Departement>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Departement), "Pas une Departement");
            Departement.DepartementCode = ((Departement)actionResult.Value).DepartementCode;
            Assert.AreEqual(Departement, (Departement)actionResult.Value, "Departements pas identiques");

        }
        [TestMethod()]
        public void PostDepartement_CodeInsee_CreationFailed()
        {

            //Act
            var result = _controller.PostDepartement(Departement).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Departement), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            string id = "2";//Mauvais ID

            // Act
            var result = await _controller.PutDepartement(id, Departement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {

            string id = "1"; //BonID

            // Act
            var result = await _controller.PutDepartement(id, Departement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteDepartementTest()
        {

            //Act
            var result = _controller.GetDepartement("1");

            //Existence
            var actionResult = result.Result as ActionResult<Departement>;
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Departement), "Pas une Departement");

            //Act
            var resultDest = _controller.DeleteDepartement("1");

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<Departement>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "Departement pas null");
        }
    }
}