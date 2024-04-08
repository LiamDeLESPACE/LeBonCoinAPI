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
    public class AdressesControllerTests
    {
        private AdressesController _controller;
        private DataContext _context;
        private IRepositoryAdresse<Adresse> _dataRepository;

        //Arrange
        Adresse adresse;
        List<Adresse> testListe;

        public AdressesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=51.83.36.122; port=5432; Database=sa23; uid=sa23; password=idkY3t?; SearchPath=sae;");
            _context = new DataContext(builder.Options);
            _dataRepository = new AdresseManager(_context);
            _controller = new AdressesController(_dataRepository);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
             adresse = new Adresse { AdresseId = 1, CodeInsee = "74002", Rue = "Rue de Cornier", Numero = 15 };

             testListe = new List<Adresse>();
             testListe.Add(new Adresse { CodeInsee = "74002", Rue = "Chemin de Cret-Vial", Numero = 47 });
             testListe.Add(new Adresse { CodeInsee = "74002", Rue = "Rue de la capetaz", Numero = 1 });
             
             
        }

        [TestMethod()]
        public void GetAdresse_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var mockRepository = new Mock<IRepositoryAdresse<Adresse>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new AdressesController(mockRepository.Object);

            var result = _controller.GetAdresse(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Adresse>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Adresse>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Adresse), "Pas une adresse");
            Assert.AreEqual(adresse, (Adresse)actionResult.Value, "Adresse pas identiques");
        }

        [TestMethod()]
        public void GetAdresse_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var mockRepository = new Mock<IRepositoryAdresse<Adresse>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new AdressesController(mockRepository.Object);

            var result = _controller.GetAdresse(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Adresse>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Adresse pas null");
        }

        [TestMethod()]
        public void GetAdresses_ReturnsRightItems()
        {

            //Act
            var mockRepository = new Mock<IRepositoryAdresse<Adresse>>();
            mockRepository.Setup(x => x.GetAll()).Returns(testListe);
            var userController = new AdressesController(mockRepository.Object);

            var result = _controller.GetAdresses();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Adresse>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Adresse>> actionResult = result.Result as ActionResult<IEnumerable<Adresse>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.AdresseId <= 2).ToList(), "Pas les mêmes adresses");
        }

        [TestMethod()]
        public void PostAdresse_ModelValidated_CreationOK()
        {

            var mockRepository = new Mock<IRepositoryAdresse<Adresse>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new AdressesController(mockRepository.Object);
            //Act
            var result = _controller.PostAdresse(adresse).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Adresse>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Adresse), "Pas une adresse");
            adresse.CodeInsee = ((Adresse)actionResult.Value).CodeInsee;
            Assert.AreEqual(adresse, (Adresse)actionResult.Value, "Adresses pas identiques");

        }
        [TestMethod()]
        public void PostAdresse_CreationFailed()
        {

            var mockRepository = new Mock<IRepositoryAdresse<Adresse>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new AdressesController(mockRepository.Object);
            //Act
            var result = _controller.PostAdresse(adresse).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Adresse), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepositoryAdresse<Adresse>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new AdressesController(mockRepository.Object);

            // Arrange
            int id = 2;//Mauvais ID

            // Act
            var result = await _controller.PutAdresse(id, adresse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {
            var mockRepository = new Mock<IRepositoryAdresse<Adresse>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new AdressesController(mockRepository.Object);

            int id = 1; //BonID

            // Act
            var result = await _controller.PutAdresse(id, adresse);

            // Assert
            Assert.IsInstanceOfType(result,typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteAdminTest()
        {
            var mockRepository = new Mock<IRepositoryAdresse<Adresse>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new AdressesController(mockRepository.Object);

            //Act
            var resultDest = _controller.DeleteAdresse(1);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<Adresse>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "Adresse pas null");
        }
    }
}