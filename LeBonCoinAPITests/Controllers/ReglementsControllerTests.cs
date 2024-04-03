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
    public class ReglementsControllerTests
    {
        private ReglementsController _controller;

        private Mock<DataContext> _context;

        //Arrange
        Reglement reglement;
        List<Reglement> testListe;

        public ReglementsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=localhost; port=5432; Database=LeBonCoinSAE; uid=postgres; password=postgres;");
            _context = new Mock<DataContext>();
            _controller = new ReglementsController(_context.Object);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            reglement = new Reglement { ReglementId= "RG-66DFB5YX5KT3WWXA582HIN-567UY976G32K16T0101LV051", ReservationId=1 };

            testListe = new List<Reglement>();
            testListe.Add(reglement);
            testListe.Add(new Reglement { ReglementId= "RG-78YZT4RY6RI3SYPA318SKB-833XK656N13D60X7250XL595", ReservationId=2 });

        }

        [TestMethod()]
        public void GetReglement_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var result = _controller.GetReglement("RG-66DFB5YX5KT3WWXA582HIN-567UY976G32K16T0101LV051");

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Reglement>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Reglement>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Reglement), "Pas une Reglement");
            Assert.AreEqual(reglement, (Reglement)actionResult.Value, "Reglement pas identiques");
        }

        [TestMethod()]
        public void GetReglement_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var result = _controller.GetReglement("RG-ZOBFB5YX5KT3WWXA582HIN-567UY976G32K16T0101LDBHG");

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Reglement>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Reglement pas null");
        }

        [TestMethod()]
        public void GetReglements_ReturnsRightItems()
        {

            //Act
            var result = _controller.GetReglements();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Reglement>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Reglement>> actionResult = result.Result as ActionResult<IEnumerable<Reglement>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.ReservationId <= 2).ToList(), "Pas les mêmes Reglements");
        }

        [TestMethod()]
        public void PostReglement_ModelValidated_CreationOK()
        {

            //Act
            var result = _controller.PostReglement(reglement).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Reglement>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Reglement), "Pas une Reglement");
            reglement.ReglementId = ((Reglement)actionResult.Value).ReglementId;
            Assert.AreEqual(reglement, (Reglement)actionResult.Value, "Reglements pas identiques");

        }
        [TestMethod()]
        public void PostReglement_CodeInsee_CreationFailed()
        {

            //Act
            var result = _controller.PostReglement(reglement).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Reglement), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            string id = "RG-ZOBFB5YX5KT3WWXA582HIN-567UY976G32K16T0101LDBHG";//Mauvais ID

            // Act
            var result = await _controller.PutReglement(id, reglement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {

            string id = "RG-66DFB5YX5KT3WWXA582HIN-567UY976G32K16T0101LV051";//Bon ID

            // Act
            var result = await _controller.PutReglement(id, reglement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteReglementTest()
        {

            //Act
            var result = _controller.GetReglement("RG-66DFB5YX5KT3WWXA582HIN-567UY976G32K16T0101LV051");

            //Existence
            var actionResult = result.Result as ActionResult<Reglement>;
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Reglement), "Pas une Reglement");

            //Act
            var resultDest = _controller.DeleteReglement("RG-66DFB5YX5KT3WWXA582HIN-567UY976G32K16T0101LV051");

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<Reglement>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "Reglement pas null");
        }
    }
}