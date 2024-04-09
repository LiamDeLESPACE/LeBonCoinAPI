using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeBonCoinAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Castle.Components.DictionaryAdapter.Xml;
using Moq;
using LeBonCoinAPI.DataManager;

namespace LeBonCoinAPI.Controllers.Tests
{
    [TestClass()]
    public class SecteurActivitesControllerTests
    {
        private SecteurActivitesController _controller;
        private DataContext _context;
        private IRepository<SecteurActivite> _dataRepository;

        //Arrange
        SecteurActivite secteurActivite;
        SecteurActivite secteurActiviteUpdated;
        List<SecteurActivite> testListe;

        public SecteurActivitesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=51.83.36.122; port=5432; Database=sa23; uid=sa23; password=idkY3t?; SearchPath=sae;");
            _context = new DataContext(builder.Options);
            _dataRepository = new SecteurActiviteManager(_context);
            _controller = new SecteurActivitesController(_dataRepository);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            secteurActivite = new SecteurActivite { SecteurId=1, NomSecteur="Technologie de l'information" };
            secteurActiviteUpdated = new SecteurActivite { SecteurId=1, NomSecteur="Technologie" };

            testListe = new List<SecteurActivite>();
            testListe.Add(secteurActivite);
            testListe.Add(new SecteurActivite { SecteurId = 2, NomSecteur="Environnement et développement durable" });

        }

        [TestMethod()]
        public void GetSecteurActivite_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var mockRepository = new Mock<IRepository<SecteurActivite>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new SecteurActivitesController(mockRepository.Object);

            var result = userController.GetSecteurActivite(1);

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
            var mockRepository = new Mock<IRepository<SecteurActivite>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new SecteurActivitesController(mockRepository.Object);

            var result = userController.GetSecteurActivite(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<SecteurActivite>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "SecteurActivite pas null");
        }

        [TestMethod()]
        public void GetSecteurActivites_ReturnsRightItems()
        {
            var mockRepository = new Mock<IRepository<SecteurActivite>>();
            mockRepository.Setup(x => x.GetAll().Result).Returns(testListe);
            var userController = new SecteurActivitesController(mockRepository.Object);

            //Act
            var result = userController.GetSecteurActivites();

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
            var mockRepository = new Mock<IRepository<SecteurActivite>>();
            //mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new SecteurActivitesController(mockRepository.Object);

            //Act
            var result = userController.PostSecteurActivite(secteurActivite).Result;

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
            var mockRepository = new Mock<IRepository<SecteurActivite>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new SecteurActivitesController(mockRepository.Object);

            //Act
            var result = userController.PostSecteurActivite(secteurActivite).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(SecteurActivite), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public void Put_WithInvalidId_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepository<SecteurActivite>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(secteurActivite);
            var userController = new SecteurActivitesController(mockRepository.Object);


            // Act
            var result = userController.PutSecteurActivite(4, secteurActiviteUpdated);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void Put_WithValidId_ReturnsNoContent()
        {
            var mockRepository = new Mock<IRepository<SecteurActivite>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(secteurActivite);
            var userController = new SecteurActivitesController(mockRepository.Object);



            // Act
            var result = userController.PutSecteurActivite(1, secteurActiviteUpdated);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteSecteurActiviteTest()
        {
            var mockRepository = new Mock<IRepository<SecteurActivite>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new SecteurActivitesController(mockRepository.Object);

            //Act
            var resultDest = userController.DeleteSecteurActivite(1);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(NoContentResult), "Pas un NoContentResult");

        }
    }
}