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
    public class FavorisControllerTests
    {/*
        private FavorisController _controller;

        private Mock<DataContext> _context;

        //Arrange
        Favoris favoris;
        List<Favoris> testListe;

        public FavorisControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=localhost; port=5432; Database=LeBonCoinSAE; uid=postgres; password=postgres;");
            _context = new Mock<DataContext>();
            _controller = new FavorisController(_context.Object);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            favoris = new Favoris { AnnonceId=31, ProfilId = 90 };

            testListe = new List<Favoris>();
            testListe.Add(new Favoris { AnnonceId = 31, ProfilId = 90 });
            testListe.Add(new Favoris { AnnonceId = 12, ProfilId = 90 });

        }

        [TestMethod()]
        public void GetFavoris_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var result = _controller.GetFavoris(37);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Favoris>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Favoris>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Favoris), "Pas une Favoris");
            Assert.AreEqual(favoris, (Favoris)actionResult.Value, "Favoris pas identiques");
        }

        [TestMethod()]
        public void GetFavoris_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var result = _controller.GetFavoris(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Favoris>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Favoris pas null");
        }

        [TestMethod()]
        public void GetFavoris_ReturnsRightItems()
        {

            //Act
            var result = _controller.GetlesFavoris();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Favoris>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Favoris>> actionResult = result.Result as ActionResult<IEnumerable<Favoris>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.ProfilId == 90).ToList(), "Pas les mêmes Favoris");
        }

        [TestMethod()]
        public void PostFavoris_ModelValidated_CreationOK()
        {

            //Act
            var result = _controller.PostFavoris(favoris).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Favoris>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Favoris), "Pas une Favoris");
            favoris.ProfilId = ((Favoris)actionResult.Value).ProfilId;
            Assert.AreEqual(favoris, (Favoris)actionResult.Value, "Favoris pas identiques");

        }
        [TestMethod()]
        public void PostFavoris_CodeInsee_CreationFailed()
        {

            //Act
            var result = _controller.PostFavoris(favoris).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Favoris), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            int id = 2;//Mauvais ID

            // Act
            var result = await _controller.PutFavoris(id, favoris);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {

            int id = 31; //BonID

            // Act
            var result = await _controller.PutFavoris(id, favoris);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteFavorisTest()
        {

            //Act
            var result = _controller.GetFavoris(31);

            //Existence
            var actionResult = result.Result as ActionResult<Favoris>;
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Favoris), "Pas une Favoris");

            //Act
            var resultDest = _controller.DeleteFavoris(31);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<Favoris>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "Favoris pas null");
        }*/
    }
}