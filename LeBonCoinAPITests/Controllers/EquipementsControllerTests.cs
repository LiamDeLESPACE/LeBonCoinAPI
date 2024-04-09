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
    public class EquipementsControllerTests
    {
        private EquipementsController _controller;
        private DataContext _context;
        private IRepository<Equipement> _dataRepository;


        //Arrange
        Equipement equipement;
        List<Equipement> testListe;

        public EquipementsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=51.83.36.122; port=5432; Database=sa23; uid=sa23; password=idkY3t?; SearchPath=sae;");
            _context = new DataContext(builder.Options);
            _dataRepository = new EquipementManager(_context);
            _controller = new EquipementsController(_dataRepository);
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
            var mockRepository = new Mock<IRepository<Equipement>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new EquipementsController(mockRepository.Object);

            var result = userController.GetEquipement(1);

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
            var mockRepository = new Mock<IRepository<Equipement>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new EquipementsController(mockRepository.Object);

            var result = userController.GetEquipement(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Equipement>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Equipement pas null");
        }

        [TestMethod()]
        public void GetEquipements_ReturnsRightItems()
        {
            var mockRepository = new Mock<IRepository<Equipement>>();
            mockRepository.Setup(x => x.GetAll().Result).Returns(testListe);
            var userController = new EquipementsController(mockRepository.Object);

            //Act
            var result = userController.GetEquipements();

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
            var mockRepository = new Mock<IRepository<Equipement>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new EquipementsController(mockRepository.Object);

            //Act
            var result = userController.PostEquipement(equipement).Result;

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
            var mockRepository = new Mock<IRepository<Equipement>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new EquipementsController(mockRepository.Object);

            //Act
            var result = userController.PostEquipement(equipement).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Equipement), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepository<Equipement>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new EquipementsController(mockRepository.Object);

            // Arrange
            int id = 2;//Mauvais ID

            // Act
            var result = await userController.PutEquipement(id, equipement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {
            var mockRepository = new Mock<IRepository<Equipement>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new EquipementsController(mockRepository.Object);

            int id = 1; //BonID

            // Act
            var result = await userController.PutEquipement(id, equipement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteEquipementTest()
        {
            var mockRepository = new Mock<IRepository<Equipement>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new EquipementsController(mockRepository.Object);

            //Act
            var result = userController.GetEquipement(1);

            //Existence
            var actionResult = result.Result as ActionResult<Equipement>;
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Equipement), "Pas une Equipement");

            //Act
            var resultDest = userController.DeleteEquipement(1);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(NoContentResult), "Pas un NoContentResult");
        }
    }
}