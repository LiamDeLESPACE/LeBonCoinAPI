using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class VillesControllerTests
    {
        private VillesController _controller;

        private Mock<DataContext> _context;

        //Arrange
        Ville ville;
        List<Ville> testListe;

        public VillesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=localhost; port=5432; Database=LeBonCoinSAE; uid=postgres; password=postgres;");
            _context = new Mock<DataContext>();
            _controller = new VillesController(_context.Object);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            ville = new Ville { CodeInsee="01004", DepartementCode= "1", Nom = "AMBERIEU EN BUGEY", CodePostal="01500" };

            testListe = new List<Ville>();
            testListe.Add(ville);
            testListe.Add(new Ville { CodeInsee = "01005", DepartementCode = "1", Nom = "AMBERIEUX EN DOMBES", CodePostal = "01330" });

        }

        [TestMethod()]
        public void GetVille_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var result = _controller.GetVille("01004");

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Ville>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Ville>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Ville), "Pas une Ville");
            Assert.AreEqual(ville, (Ville)actionResult.Value, "Ville pas identiques");
        }

        [TestMethod()]
        public void GetVille_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var result = _controller.GetVille("00000");

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Ville>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Ville pas null");
        }

        [TestMethod()]
        public void GetVilles_ReturnsRightItems()
        {

            //Act
            var result = _controller.GetVilles();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Ville>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Ville>> actionResult = result.Result as ActionResult<IEnumerable<Ville>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.CodeInsee == "01004" || s.CodeInsee == "01005").ToList(), "Pas les mêmes Villes");
        }

        [TestMethod()]
        public void PostVille_ModelValidated_CreationOK()
        {

            //Act
            var result = _controller.PostVille(ville).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Ville>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Ville), "Pas une Ville");
            ville.Nom = ((Ville)actionResult.Value).Nom;
            Assert.AreEqual(ville, (Ville)actionResult.Value, "Villes pas identiques");

        }
        [TestMethod()]
        public void PostVille_CodeInsee_CreationFailed()
        {

            //Act
            var result = _controller.PostVille(ville).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Ville), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            string id = "00000";//Mauvais ID

            // Act
            var result = await _controller.PutVille(id, ville);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {

            string id = "01500"; //BonID

            // Act
            var result = await _controller.PutVille(id, ville);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteVilleTest()
        {

            //Act
            var result = _controller.GetVille("01004");

            //Existence
            var actionResult = result.Result as ActionResult<Ville>;
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Ville), "Pas une Ville");

            //Act
            var resultDest = _controller.DeleteVille("01004");

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<Ville>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "Ville pas null");
        }
    }
}