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
using LeBonCoinAPI.Models.Repository;
using LeBonCoinAPI.DataManager;

namespace LeBonCoinAPI.Controllers.Tests
{
    [TestClass()]
    public class EntreprisesControllerTests
    {
        private EntreprisesController _controller;
        private DataContext _context;
        private IRepository<Entreprise> _dataRepository;

        //Arrange
        Entreprise entreprise;
        List<Entreprise> testListe;

        public EntreprisesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=51.83.36.122; port=5432; Database=sa23; uid=sa23; password=idkY3t?; SearchPath=sae;");
            _context = new DataContext(builder.Options);
            _dataRepository = new EntrepriseManager(_context);
            _controller = new EntreprisesController(_dataRepository);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            entreprise = new Entreprise { AdresseId = 7, HashMotDePasse = "$2b$12$CyFB0qqJYEd8/JM5U3psTO2E4QbiPzRtNPhLZB2qX2ylfYr.mfnU2", Telephone = "0678755447", SecteurId = 9, Siret = "91622180700043", Nom = "AlphaTech Solutions" };

            testListe = new List<Entreprise>();
            testListe.Add(new Entreprise { AdresseId = 7, HashMotDePasse = "$2b$12$CyFB0qqJYEd8/JM5U3psTO2E4QbiPzRtNPhLZB2qX2ylfYr.mfnU2", Telephone = "0678755447", SecteurId = 9, Siret = "91622180700043", Nom = "AlphaTech Solutions" });
            testListe.Add(new Entreprise { AdresseId = 8, HashMotDePasse = "$2b$12$0L6E.sIFXvp7hEimxJpDdOoT7qAK4jVSW2QX4hq/3k5y/zLZn.I9K", Telephone = "0728838233", SecteurId = 3, Siret = "66118748100061", Nom = "BrightVision Consulting" });

        }

        [TestMethod()]
        public void GetEntreprise_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var mockRepository = new Mock<IRepository<Entreprise>>();
            mockRepository.Setup(x => x.GetById(7).Result).Returns(testListe[0]);
            var userController = new EntreprisesController(mockRepository.Object);

            var result = userController.GetEntreprise(7);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Entreprise>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Entreprise>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Entreprise), "Pas une Entreprise");
            Assert.AreEqual(entreprise, (Entreprise)actionResult.Value, "Entreprise pas identiques");
        }

        [TestMethod()]
        public void GetEntreprise_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var mockRepository = new Mock<IRepository<Entreprise>>();
            mockRepository.Setup(x => x.GetById(7).Result).Returns(testListe[0]);
            var userController = new EntreprisesController(mockRepository.Object);

            var result = userController.GetEntreprise(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Entreprise>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Entreprise pas null");
        }

        [TestMethod()]
        public void GetEntreprises_ReturnsRightItems()
        {

            //Act
            var mockRepository = new Mock<IRepository<Entreprise>>();
            mockRepository.Setup(x => x.GetAll().Result).Returns(testListe);
            var userController = new EntreprisesController(mockRepository.Object);

            var result = userController.GetEntreprises();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Entreprise>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Entreprise>> actionResult = result.Result as ActionResult<IEnumerable<Entreprise>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.ProfilId <= 8).ToList(), "Pas les mêmes Entreprises");
        }

        [TestMethod()]
        public void PostEntreprise_ModelValidated_CreationOK()
        {
            var mockRepository = new Mock<IRepository<Entreprise>>();
            mockRepository.Setup(x => x.GetById(7).Result).Returns(testListe[0]);
            var userController = new EntreprisesController(mockRepository.Object);
            //Act
            var result = userController.PostEntreprise(entreprise).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Entreprise>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Entreprise), "Pas une Entreprise");
            entreprise.Siret = ((Entreprise)actionResult.Value).Siret;
            Assert.AreEqual(entreprise, (Entreprise)actionResult.Value, "Entreprises pas identiques");

        }
        [TestMethod()]
        public void PostEntreprise_CreationFailed()
        {
            var mockRepository = new Mock<IRepository<Entreprise>>();
            mockRepository.Setup(x => x.GetById(7).Result).Returns(testListe[0]);
            var userController = new EntreprisesController(mockRepository.Object);
            //Act
            var result = userController.PostEntreprise(entreprise).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Entreprise), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepository<Entreprise>>();
            mockRepository.Setup(x => x.GetById(7).Result).Returns(testListe[0]);
            var userController = new EntreprisesController(mockRepository.Object);

            // Arrange
            int id = 2;//Mauvais ID

            // Act
            var result = await userController.PutEntreprise(id, entreprise);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {
            var mockRepository = new Mock<IRepository<Entreprise>>();
            mockRepository.Setup(x => x.GetById(7).Result).Returns(testListe[0]);
            var userController = new EntreprisesController(mockRepository.Object);

            int id = 7; //BonID

            // Act
            var result = await userController.PutEntreprise(id, entreprise);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteEntrepriseTest()
        {
            var mockRepository = new Mock<IRepository<Entreprise>>();
            mockRepository.Setup(x => x.GetById(7).Result).Returns(testListe[0]);
            var userController = new EntreprisesController(mockRepository.Object);

            //Act
            var resultDest = userController.DeleteEntreprise(7);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(NoContentResult), "Pas un NoContentResult");
        }
    }
}