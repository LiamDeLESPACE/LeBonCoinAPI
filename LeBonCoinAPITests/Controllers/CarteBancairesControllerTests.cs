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
using Moq;
using LeBonCoinAPI.DataManager;

namespace LeBonCoinAPI.Controllers.Tests
{
    [TestClass()]
    public class CarteBancairesControllerTests
    {

        private CarteBancairesController _controller;
        private DataContext _context;
        private IRepository<CarteBancaire> _dataRepository;

        //Arrange
        CarteBancaire carteBancaire;
        CarteBancaire carteBancaireUpdated;
        List<CarteBancaire> testListe;

        public CarteBancairesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=51.83.36.122; port=5432; Database=sa23; uid=sa23; password=idkY3t?; SearchPath=sae;");
            _context = new DataContext(builder.Options);
            _dataRepository = new CarteBancaireManager(_context);
            _controller = new CarteBancairesController(_dataRepository);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            carteBancaire = new CarteBancaire { CarteId=1, ProfilId=2, Numero= "4556173923578293" };
            carteBancaireUpdated = new CarteBancaire { CarteId=1, ProfilId=2, Numero= "4556173923578294" };

            testListe = new List<CarteBancaire>();
            testListe.Add(carteBancaire);
            testListe.Add(new CarteBancaire { CarteId = 2, ProfilId = 5, Numero = "4903422675465567" });

        }

        [TestMethod()]
        public void GetCarteBancaire_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var mockRepository = new Mock<IRepository<CarteBancaire>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new CarteBancairesController(mockRepository.Object);

            var result = userController.GetCarteBancaire(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<CarteBancaire>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<CarteBancaire>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(CarteBancaire), "Pas une CarteBancaire");
            Assert.AreEqual(carteBancaire, (CarteBancaire)actionResult.Value, "CarteBancaire pas identiques");
        }

        [TestMethod()]
        public void GetCarteBancaire_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var mockRepository = new Mock<IRepository<CarteBancaire>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new CarteBancairesController(mockRepository.Object);

            var result = userController.GetCarteBancaire(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<CarteBancaire>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "CarteBancaire pas null");
        }

        [TestMethod()]
        public void GetCarteBancaires_ReturnsRightItems()
        {

            //Act
            var mockRepository = new Mock<IRepository<CarteBancaire>>();
            mockRepository.Setup(x => x.GetAll().Result).Returns(testListe);
            var userController = new CarteBancairesController(mockRepository.Object);

            var result = userController.GetCarteBancaires();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<CarteBancaire>>), "Pas un ActionResult");
            ActionResult<IEnumerable<CarteBancaire>> actionResult = result.Result as ActionResult<IEnumerable<CarteBancaire>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.CarteId <=2).ToList(), "Pas les mêmes CarteBancaires");
        }

        [TestMethod()]
        public void PostCarteBancaire_ModelValidated_CreationOK()
        {
            var mockRepository = new Mock<IRepository<CarteBancaire>>();
            //mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new CarteBancairesController(mockRepository.Object);
            //Act
            var result = userController.PostCarteBancaire(carteBancaire).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<CarteBancaire>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(CarteBancaire), "Pas une CarteBancaire");
            carteBancaire.Numero = ((CarteBancaire)actionResult.Value).Numero;
            Assert.AreEqual(carteBancaire, (CarteBancaire)actionResult.Value, "CarteBancaires pas identiques");

        }
        [TestMethod()]
        public void PostCarteBancaire_CreationFailed()
        {
            var mockRepository = new Mock<IRepository<CarteBancaire>>();
            //mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new CarteBancairesController(mockRepository.Object);

            //Act
            var result = userController.PostCarteBancaire(carteBancaire).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(CarteBancaire), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public void Put_WithInvalidId_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepository<CarteBancaire>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(carteBancaire);
            var userController = new CarteBancairesController(mockRepository.Object);

            // Arrange
            int id = 27;//Mauvais ID

            // Act
            var result = userController.PutCarteBancaire(id, carteBancaireUpdated);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult), "Pas un BadRequestResult");
        }

        [TestMethod()]
        public void Put_WithValidId_ReturnsNoContent()
        {
            var mockRepository = new Mock<IRepository<CarteBancaire>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(carteBancaire);
            var userController = new CarteBancairesController(mockRepository.Object);

            int id = 1; //BonID

            // Act
            var result = userController.PutCarteBancaire(id, carteBancaireUpdated);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod()]
        public void DeleteCarteBancaireTest()
        {
            var mockRepository = new Mock<IRepository<CarteBancaire>>();
            mockRepository.Setup(x => x.GetById(1).Result).Returns(testListe[0]);
            var userController = new CarteBancairesController(mockRepository.Object);

            //Act
            var resultDest = userController.DeleteCarteBancaire(1);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(NoContentResult), "Pas un NoContentResult");
        }
    }
}