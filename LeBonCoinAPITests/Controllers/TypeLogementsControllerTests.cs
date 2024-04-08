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
    public class TypeLogementsControllerTests
    {
        private TypeLogementsController _controller;
        private DataContext _context;
        private IRepositoryTypeLogement<TypeLogement> _dataRepository;

        //Arrange
        TypeLogement typeLogement;
        List<TypeLogement> testListe;

        public TypeLogementsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=51.83.36.122; port=5432; Database=sa23; uid=sa23; password=idkY3t?; SearchPath=sae;");
            _context = new DataContext(builder.Options);
            _dataRepository = new TypeLogementManager(_context);
            _controller = new TypeLogementsController(_dataRepository);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            typeLogement = new TypeLogement { TypeLogementId = 1, Nom = "Appartement" };

            testListe = new List<TypeLogement>();
            testListe.Add(typeLogement);
            testListe.Add(new TypeLogement { TypeLogementId = 2, Nom = "Maison" });

        }

        [TestMethod()]
        public void GetTypeLogement_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var mockRepository = new Mock<IRepositoryTypeLogement<TypeLogement>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new TypeLogementsController(mockRepository.Object);

            var result = _controller.GetTypeLogement(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<TypeLogement>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<TypeLogement>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(TypeLogement), "Pas une TypeLogement");
            Assert.AreEqual(typeLogement, (TypeLogement)actionResult.Value, "TypeLogement pas identiques");
        }

        [TestMethod()]
        public void GetTypeLogement_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var mockRepository = new Mock<IRepositoryTypeLogement<TypeLogement>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new TypeLogementsController(mockRepository.Object);

            var result = _controller.GetTypeLogement(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<TypeLogement>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "TypeLogement pas null");
        }

        [TestMethod()]
        public void GetTypeLogements_ReturnsRightItems()
        {
            var mockRepository = new Mock<IRepositoryTypeLogement<TypeLogement>>();
            mockRepository.Setup(x => x.GetAll()).Returns(testListe);
            var userController = new TypeLogementsController(mockRepository.Object);
            //Act
            var result = _controller.GetTypeLogements();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<TypeLogement>>), "Pas un ActionResult");
            ActionResult<IEnumerable<TypeLogement>> actionResult = result.Result as ActionResult<IEnumerable<TypeLogement>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.TypeLogementId<=2).ToList(), "Pas les mêmes TypeLogements");
        }

        [TestMethod()]
        public void PostTypeLogement_ModelValidated_CreationOK()
        {

            //Act
            var mockRepository = new Mock<IRepositoryTypeLogement<TypeLogement>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new TypeLogementsController(mockRepository.Object);

            var result = _controller.PostTypeLogement(typeLogement).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypeLogement>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(TypeLogement), "Pas une TypeLogement");
            typeLogement.Nom = ((TypeLogement)actionResult.Value).Nom;
            Assert.AreEqual(typeLogement, (TypeLogement)actionResult.Value, "TypeLogements pas identiques");

        }
        [TestMethod()]
        public void PostTypeLogement_CreationFailed()
        {
            var mockRepository = new Mock<IRepositoryTypeLogement<TypeLogement>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new TypeLogementsController(mockRepository.Object);

            //Act
            var result = _controller.PostTypeLogement(typeLogement).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(TypeLogement), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepositoryTypeLogement<TypeLogement>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new TypeLogementsController(mockRepository.Object);
            // Arrange
            int id = 4;//Mauvais ID

            // Act
            var result = await _controller.PutTypeLogement(id, typeLogement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {
            var mockRepository = new Mock<IRepositoryTypeLogement<TypeLogement>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new TypeLogementsController(mockRepository.Object);

            int id = 1; //BonID

            // Act
            var result = await _controller.PutTypeLogement(id, typeLogement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteTypeLogementTest()
        {
            var mockRepository = new Mock<IRepositoryTypeLogement<TypeLogement>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new TypeLogementsController(mockRepository.Object);
            
            //Act
            var resultDest = _controller.DeleteTypeLogement(1);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<TypeLogement>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "TypeLogement pas null");
        }
    }
}