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
    public class SecteurActivitesControllerTests
    {
        private SecteurActivitesController _controller;

        private Mock<DataContext> _context;

        //Arrange
        SecteurActivite secteurActivite;
        List<SecteurActivite> testListe;

        public SecteurActivitesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=localhost; port=5432; Database=LeBonCoinSAE; uid=postgres; password=postgres;");
            _context = new Mock<DataContext>();
            _controller = new SecteurActivitesController(_context.Object);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            secteurActivite = new SecteurActivite { SecteurId=1, NomSecteur="Technologie de l'information" };

            testListe = new List<SecteurActivite>();
            testListe.Add(secteurActivite);
            testListe.Add(new SecteurActivite { SecteurId = 2, NomSecteur="Environnement et développement durable" });

        }

        [TestMethod()]
        public void GetSecteurActivite_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var result = _controller.GetSecteurActivite(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<SecteurActivite>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<SecteurActivite>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(SecteurActivite), "Pas une SecteurActivite");
            Assert.AreEqual(secteurActivite, (SecteurActivite)actionResult.Value, "SecteurActivite pas identiques");
        }

        [TestMethod()]
        public void GetSecteurActivite_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var result = _controller.GetSecteurActivite(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<SecteurActivite>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "SecteurActivite pas null");
        }

        [TestMethod()]
        public void GetSecteurActivites_ReturnsRightItems()
        {

            //Act
            var result = _controller.GetSecteurActivites();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<SecteurActivite>>), "Pas un ActionResult");
            ActionResult<IEnumerable<SecteurActivite>> actionResult = result.Result as ActionResult<IEnumerable<SecteurActivite>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.SecteurId <=2).ToList(), "Pas les mêmes SecteurActivites");
        }

        [TestMethod()]
        public void PostSecteurActivite_ModelValidated_CreationOK()
        {

            //Act
            var result = _controller.PostSecteurActivite(secteurActivite).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<SecteurActivite>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(SecteurActivite), "Pas une SecteurActivite");
            secteurActivite.NomSecteur = ((SecteurActivite)actionResult.Value).NomSecteur;
            Assert.AreEqual(secteurActivite, (SecteurActivite)actionResult.Value, "SecteurActivites pas identiques");

        }
        [TestMethod()]
        public void PostSecteurActivite_CreationFailed()
        {

            //Act
            var result = _controller.PostSecteurActivite(secteurActivite).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(SecteurActivite), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            int id = 4;//Mauvais ID

            // Act
            var result = await _controller.PutSecteurActivite(id, secteurActivite);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {

            int id = 1; //BonID

            // Act
            var result = await _controller.PutSecteurActivite(id, secteurActivite);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteSecteurActiviteTest()
        {

            //Act
            var result = _controller.GetSecteurActivite(1);

            //Existence
            var actionResult = result.Result as ActionResult<SecteurActivite>;
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(SecteurActivite), "Pas une SecteurActivite");

            //Act
            var resultDest = _controller.DeleteSecteurActivite(1);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<SecteurActivite>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "SecteurActivite pas null");
        }
    }
}