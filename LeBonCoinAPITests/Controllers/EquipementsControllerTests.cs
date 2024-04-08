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
    public class EquipementsControllerTests
    {
        private EquipementsController _controller;

        private Mock<DataContext> _context;

        //Arrange
        Equipement equipement;
        List<Equipement> testListe;

        public EquipementsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=localhost; port=5432; Database=LeBonCoinSAE; uid=postgres; password=postgres;");
            _context = new Mock<DataContext>();
            _controller = new EquipementsController(_context.Object);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            equipement = new Equipement { EquipementId=1, TypeEquipementId =1, Nom="Wifi gratuit" };

            testListe = new List<Equipement>();
            testListe.Add(new Equipement { EquipementId = 1, TypeEquipementId = 1, Nom = "Wifi gratuit" });
            testListe.Add(new Equipement { EquipementId = 2, TypeEquipementId = 1, Nom = "Télévision" });

        }

        [TestMethod()]
        public void GetEquipement_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var result = _controller.GetEquipement(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Equipement>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Equipement>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Equipement), "Pas une Equipement");
            Assert.AreEqual(equipement, (Equipement)actionResult.Value, "Equipement pas identiques");
        }

        [TestMethod()]
        public void GetEquipement_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var result = _controller.GetEquipement(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Equipement>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Equipement pas null");
        }

        [TestMethod()]
        public void GetEquipements_ReturnsRightItems()
        {

            //Act
            var result = _controller.GetEquipements();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Equipement>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Equipement>> actionResult = result.Result as ActionResult<IEnumerable<Equipement>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.EquipementId <= 2).ToList(), "Pas les mêmes Equipements");
        }

        [TestMethod()]
        public void PostEquipement_ModelValidated_CreationOK()
        {

            //Act
            var result = _controller.PostEquipement(equipement).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Equipement>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Equipement), "Pas une Equipement");
            equipement.Nom = ((Equipement)actionResult.Value).Nom;
            Assert.AreEqual(equipement, (Equipement)actionResult.Value, "Equipements pas identiques");

        }
        [TestMethod()]
        public void PostEquipement_CreationFailed()
        {

            //Act
            var result = _controller.PostEquipement(equipement).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Equipement), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            int id = 2;//Mauvais ID

            // Act
            var result = await _controller.PutEquipement(id, equipement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {

            int id = 1; //BonID

            // Act
            var result = await _controller.PutEquipement(id, equipement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteEquipementTest()
        {

            //Act
            var result = _controller.GetEquipement(1);

            //Existence
            var actionResult = result.Result as ActionResult<Equipement>;
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Equipement), "Pas une Equipement");

            //Act
            var resultDest = _controller.DeleteEquipement(1);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<Equipement>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "Equipement pas null");
        }
    }
}