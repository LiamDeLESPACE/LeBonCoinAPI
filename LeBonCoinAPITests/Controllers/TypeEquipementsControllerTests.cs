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
    public class TypeEquipementsControllerTests
    {
        private TypeEquipementsController _controller;
        private DataContext _context;
        private IRepository<TypeEquipement> _dataRepository;

        //Arrange
        TypeEquipement typeEquipement;
        TypeEquipement typeEquipementUpdated;
        List<TypeEquipement> testListe;

        public TypeEquipementsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=51.83.36.122; port=5432; Database=sa23; uid=sa23; password=idkY3t?; SearchPath=sae;");
            _context = new DataContext(builder.Options);
            _dataRepository = new TypeEquipementManager(_context);
            _controller = new TypeEquipementsController(_dataRepository);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            typeEquipement = new TypeEquipement { TypeEquipementId=1, Nom="Equipements" };
            typeEquipementUpdated = new TypeEquipement { TypeEquipementId=1, Nom="Equipement" };

            testListe = new List<TypeEquipement>();
            testListe.Add(typeEquipement);
            testListe.Add(new TypeEquipement { TypeEquipementId=2, Nom="Extérieur" });

        }

        [TestMethod()]
        public void GetTypeEquipement_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var mockRepository = new Mock<IRepository<TypeEquipement>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new TypeEquipementsController(mockRepository.Object);

            var result = userController.GetTypeEquipement(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<TypeEquipement>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<TypeEquipement>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(TypeEquipement), "Pas une TypeEquipement");
            Assert.AreEqual(typeEquipement, (TypeEquipement)actionResult.Value, "TypeEquipement pas identiques");
        }

        [TestMethod()]
        public void GetTypeEquipement_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var mockRepository = new Mock<IRepository<TypeEquipement>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new TypeEquipementsController(mockRepository.Object);

            var result = userController.GetTypeEquipement(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<TypeEquipement>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "TypeEquipement pas null");
        }

        [TestMethod()]
        public void GetTypeEquipements_ReturnsRightItems()
        {
            var mockRepository = new Mock<IRepository<TypeEquipement>>();
            mockRepository.Setup(x => x.GetAll().Result).Returns(testListe);
            var userController = new TypeEquipementsController(mockRepository.Object);

            //Act
            var result = userController.GetTypeEquipements();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<TypeEquipement>>), "Pas un ActionResult");
            ActionResult<IEnumerable<TypeEquipement>> actionResult = result.Result as ActionResult<IEnumerable<TypeEquipement>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.TypeEquipementId <= 2).ToList(), "Pas les mêmes TypeEquipements");
        }

        [TestMethod()]
        public void PostTypeEquipement_ModelValidated_CreationOK()
        {
            var mockRepository = new Mock<IRepository<TypeEquipement>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new TypeEquipementsController(mockRepository.Object);

            //Act
            var result = userController.PostTypeEquipement(typeEquipement).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<TypeEquipement>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(TypeEquipement), "Pas une TypeEquipement");
            typeEquipement.Nom = ((TypeEquipement)actionResult.Value).Nom;
            Assert.AreEqual(typeEquipement, (TypeEquipement)actionResult.Value, "TypeEquipements pas identiques");

        }
        [TestMethod()]
        public void PostTypeEquipement_CreationFailed()
        {
            var mockRepository = new Mock<IRepository<TypeEquipement>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new TypeEquipementsController(mockRepository.Object);

            //Act
            var result = userController.PostTypeEquipement(typeEquipement).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(TypeEquipement), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public void Put_WithInvalidId_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepository<TypeEquipement>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(typeEquipement);
            var userController = new TypeEquipementsController(mockRepository.Object);

            

            // Act
            var result = userController.PutTypeEquipement(27, typeEquipementUpdated);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult), "Pas un BadRequestResult");
        }

        [TestMethod()]
        public void Put_WithValidId_ReturnsNoContent()
        {
            var mockRepository = new Mock<IRepository<TypeEquipement>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(typeEquipement);
            var userController = new TypeEquipementsController(mockRepository.Object);

           

            // Act
            var result = userController.PutTypeEquipement(1, typeEquipementUpdated);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod()]
        public void DeleteTypeEquipementTest()
        {
            var mockRepository = new Mock<IRepository<TypeEquipement>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new TypeEquipementsController(mockRepository.Object);

            //Act
            var result = userController.GetTypeEquipement(1);

            //Existence
            var actionResult = result.Result as ActionResult<TypeEquipement>;
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(TypeEquipement), "Pas une TypeEquipement");

            //Act
            var resultDest = userController.DeleteTypeEquipement(1);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(NoContentResult), "Pas un NoContentResult");
        }
    }
}