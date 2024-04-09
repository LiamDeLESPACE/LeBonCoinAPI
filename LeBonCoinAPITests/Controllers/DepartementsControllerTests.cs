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
    public class DepartementsControllerTests
    {

        private DepartementsController _controller;
        private DataContext _context;
        private IRepositoryDepartement<Departement> _dataRepository;

        //Arrange
        Departement departement;
        Departement departementUpdated;
        List<Departement> testListe;

        public DepartementsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=51.83.36.122; port=5432; Database=sa23; uid=sa23; password=idkY3t?; SearchPath=sae;");
            _context = new DataContext(builder.Options);
            _dataRepository = new DepartementManager(_context);
            _controller = new DepartementsController(_dataRepository);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            departement = new Departement { DepartementCode = "1", Nom = "Ain" };
            departementUpdated = new Departement { DepartementCode = "1", Nom = "Aine" };

            testListe = new List<Departement>();
            testListe.Add(new Departement { DepartementCode = "1", Nom = "Ain" });
            testListe.Add(new Departement { DepartementCode = "2", Nom = "Aisne" });


        }

        [TestMethod()]
        public void GetDepartement_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var mockRepository = new Mock<IRepositoryDepartement<Departement>>();
            mockRepository.Setup(x => x.GetByCode("1").Result).Returns(testListe[0]);
            var userController = new DepartementsController(mockRepository.Object);
            
            var result = userController.GetDepartementByCode("1");

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Departement>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Departement>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Departement), "Pas une Departement");
            Assert.AreEqual(testListe[0], actionResult.Value as Departement, "Departement pas identiques");
        }

        [TestMethod()]
        public void GetDepartement_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var mockRepository = new Mock<IRepositoryDepartement<Departement>>();
            mockRepository.Setup(x => x.GetByCode("1").Result).Returns(testListe[0]);
            var userController = new DepartementsController(mockRepository.Object);

            var result = userController.GetDepartementByCode("0");

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Departement>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Departement pas null");
        }

        [TestMethod()]
        public void GetDepartements_ReturnsRightItems()
        {

            //Act
            var mockRepository = new Mock<IRepositoryDepartement<Departement>>();
            mockRepository.Setup(x => x.GetAll().Result).Returns(testListe);
            var userController = new DepartementsController(mockRepository.Object);

            var result = userController.GetDepartements();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Departement>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Departement>> actionResult = result.Result as ActionResult<IEnumerable<Departement>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsTrue(testListe.Any(d => d.DepartementCode == "1"));
            Assert.IsTrue(testListe.Any(d => d.DepartementCode == "2"));
        }

        [TestMethod()]
        public void PostDepartement_ModelValidated_CreationOK()
        {

            var mockRepository = new Mock<IRepositoryDepartement<Departement>>();
            //mockRepository.Setup(x => x.GetByString("1").Result).Returns(testListe[0]);
            var userController = new DepartementsController(mockRepository.Object);
            //Act
            var result = userController.PostDepartement(departement).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Departement>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Departement), "Pas une Departement");
            departement.DepartementCode = ((Departement)actionResult.Value).DepartementCode;
            Assert.AreEqual(departement, (Departement)actionResult.Value, "Departements pas identiques");

        }
        [TestMethod()]
        public void PostDepartement_CreationFailed()
        {
            var mockRepository = new Mock<IRepositoryDepartement<Departement>>();
            //mockRepository.Setup(x => x.GetByString("1").Result).Returns(testListe[0]);
            var userController = new DepartementsController(mockRepository.Object);

            //Act
            var result = userController.PostDepartement(departement).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Departement), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public void Put_WithInvalidId_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepositoryDepartement<Departement>>();
            mockRepository.Setup(x => x.GetByCode("1").Result).Returns(departement);
            var userController = new DepartementsController(mockRepository.Object);

            // Arrange
            string id = "6";//Mauvais ID

            // Act
            var result = userController.PutDepartement(id, departementUpdated);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult), "Pas un BadRequestResult");
        }

        [TestMethod()]
        public void Put_WithValidId_ReturnsNoContent()
        {
            var mockRepository = new Mock<IRepositoryDepartement<Departement>>();
            mockRepository.Setup(x => x.GetByCode("1").Result).Returns(departement);
            var userController = new DepartementsController(mockRepository.Object);

            string id = "1"; //BonID

            // Act
            var result = userController.PutDepartement(id, departementUpdated);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod()]
        public void DeleteDepartementTest()
        {
            var mockRepository = new Mock<IRepositoryDepartement<Departement>>();
            mockRepository.Setup(x => x.GetByCode("1").Result).Returns(testListe[0]);
            var userController = new DepartementsController(mockRepository.Object);

            //Act
            var resultDest = userController.DeleteDepartement("1");

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(NoContentResult), "Pas un NoContentResult");
        }
    }
}