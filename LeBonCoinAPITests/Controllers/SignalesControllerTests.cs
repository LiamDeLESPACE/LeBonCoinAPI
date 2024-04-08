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
    public class SignalesControllerTests
    {
        private SignalesController _controller;

        private Mock<DataContext> _context;

        //Arrange
        Signale signale;
        List<Signale> testListe;

        public SignalesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=localhost; port=5432; Database=LeBonCoinSAE; uid=postgres; password=postgres;");
            _context = new Mock<DataContext>();
            _controller = new SignalesController(_context.Object);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            signale = new Signale { ProfilId=13, AnnonceId=30 };

            testListe = new List<Signale>();
            testListe.Add(new Signale { ProfilId = 13, AnnonceId = 30 });
            testListe.Add(new Signale { ProfilId = 39, AnnonceId = 30 });

        }

        [TestMethod()]
        public void GetSignale_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var result = _controller.GetSignale(13);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Signale>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Signale>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Signale), "Pas une Signale");
            Assert.AreEqual(signale, (Signale)actionResult.Value, "Signale pas identiques");
        }

        [TestMethod()]
        public void GetSignale_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var result = _controller.GetSignale(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Signale>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Signale pas null");
        }

        [TestMethod()]
        public void GetSignales_ReturnsRightItems()
        {

            //Act
            var result = _controller.GetSignales();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Signale>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Signale>> actionResult = result.Result as ActionResult<IEnumerable<Signale>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.AnnonceId==30).ToList(), "Pas les mêmes Signales");
        }

        [TestMethod()]
        public void PostSignale_ModelValidated_CreationOK()
        {

            //Act
            var result = _controller.PostSignale(signale).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Signale>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Signale), "Pas une Signale");
            signale.AnnonceId = ((Signale)actionResult.Value).AnnonceId;
            Assert.AreEqual(signale, (Signale)actionResult.Value, "Signales pas identiques");

        }
        [TestMethod()]
        public void PostSignale_CreationFailed()
        {

            //Act
            var result = _controller.PostSignale(signale).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Signale), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            int id = 0;//Mauvais ID

            // Act
            var result = await _controller.PutSignale(id, signale);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {

            int id = 13; //BonID

            // Act
            var result = await _controller.PutSignale(id, signale);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteSignaleTest()
        {

            //Act
            var result = _controller.GetSignale(13);

            //Existence
            var actionResult = result.Result as ActionResult<Signale>;
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Signale), "Pas une Signale");

            //Act
            var resultDest = _controller.DeleteSignale(13);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<Signale>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "Signale pas null");
        }
    }
}