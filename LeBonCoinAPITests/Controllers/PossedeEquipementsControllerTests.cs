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
    public class PossedeEquipementControllerTests
    {
        private PossedeEquipementsController _controller;
        private Mock<DataContext> _context;

        //Arrange
        PossedeEquipement possedeEquipement;
        List<PossedeEquipement> testListe;

        public PossedeEquipementControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=localhost; port=5432; Database=LeBonCoinSAE; uid=postgres; password=postgres;");
            _context = new Mock<DataContext>();
            _controller = new PossedeEquipementsController(_context.Object);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            possedeEquipement = new PossedeEquipement { AnnonceId=43, EquipementId=17 };

            testListe = new List<PossedeEquipement>();
            testListe.Add(new PossedeEquipement { AnnonceId = 43, EquipementId = 17 });
            testListe.Add(new PossedeEquipement { AnnonceId = 17, EquipementId = 17 });

        }

        [TestMethod()]
        public void GetPossedeEquipement_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var result = _controller.GetPossedeEquipement(43);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<PossedeEquipement>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<PossedeEquipement>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(PossedeEquipement), "Pas une PossedeEquipement");
            Assert.AreEqual(possedeEquipement, (PossedeEquipement)actionResult.Value, "PossedeEquipement pas identiques");
        }

        [TestMethod()]
        public void GetPossedeEquipement_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var result = _controller.GetPossedeEquipement(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<PossedeEquipement>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "PossedeEquipement pas null");
        }

        [TestMethod()]
        public void GetPossedeEquipement_ReturnsRightItems()
        {

            //Act
            var result = _controller.GetPossedeEquipements();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<PossedeEquipement>>), "Pas un ActionResult");
            ActionResult<IEnumerable<PossedeEquipement>> actionResult = result.Result as ActionResult<IEnumerable<PossedeEquipement>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.EquipementId== 17).ToList(), "Pas les mêmes PossedeEquipement");
        }

        [TestMethod()]
        public void PostPossedeEquipement_ModelValidated_CreationOK()
        {

            //Act
            var result = _controller.PostPossedeEquipement(possedeEquipement).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<PossedeEquipement>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(PossedeEquipement), "Pas une PossedeEquipement");
            possedeEquipement.EquipementId = ((PossedeEquipement)actionResult.Value).EquipementId;
            Assert.AreEqual(possedeEquipement, (PossedeEquipement)actionResult.Value, "PossedeEquipement pas identiques");

        }
        [TestMethod()]
        public void PostPossedeEquipement_CodeInsee_CreationFailed()
        {

            //Act
            var result = _controller.PostPossedeEquipement(possedeEquipement).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(PossedeEquipement), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            int id = 2;//Mauvais ID

            // Act
            var result = await _controller.PutPossedeEquipement(id, possedeEquipement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {

            int id = 43; //BonID

            // Act
            var result = await _controller.PutPossedeEquipement(id, possedeEquipement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeletePossedeEquipementTest()
        {

            //Act
            var result = _controller.GetPossedeEquipement(43);

            //Existence
            var actionResult = result.Result as ActionResult<PossedeEquipement>;
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(PossedeEquipement), "Pas une PossedeEquipement");

            //Act
            var resultDest = _controller.DeletePossedeEquipement(43);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<PossedeEquipement>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "PossedeEquipement pas null");
        }
    }
}