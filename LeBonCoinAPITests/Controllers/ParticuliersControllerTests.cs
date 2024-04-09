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
    public class ParticuliersControllerTests
    {
        private ParticuliersController _controller;
        private DataContext _context;
        private IRepository<Particulier> _dataRepository;

        //Arrange
        Particulier particulier;
        List<Particulier> testListe;

        public ParticuliersControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=51.83.36.122; port=5432; Database=sa23; uid=sa23; password=idkY3t?; SearchPath=sae;");
            _context = new DataContext(builder.Options);
            _dataRepository = new ParticulierManager(_context);
            _controller = new ParticuliersController(_dataRepository);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            particulier = new Particulier { ProfilId = 27, AdresseId = 27, HashMotDePasse = "$2b$12$QSHB4I9zUzSN/VEKN.z2deBfzJfJadFFPmo2/DdA5TUTGxv6e1pye", Telephone = "0686133377", Email = "aarchambault@hotmail.com", Civilite = "H", Nom = "Archambault", Prenom = "Amela", DateNaissance = DateTime.Parse("1993-07-17") };

            testListe = new List<Particulier>();
            testListe.Add(new Particulier { ProfilId = 27, HashMotDePasse = "$2b$12$QSHB4I9zUzSN/VEKN.z2deBfzJfJadFFPmo2/DdA5TUTGxv6e1pye", Telephone = "0686133377", Email = "aarchambault@hotmail.com", Civilite = "H", Nom = "Archambault", Prenom = "Amela", DateNaissance = DateTime.Parse("1993-07-17") });
            testListe.Add(new Particulier { ProfilId = 28, HashMotDePasse = "$2b$12$zGY2QWpdzqUqQZWDsIzv2.OqJq3GBvdF9Amzj9z7i8B21mzBqYSLq", Telephone = "0625642782", Email = "f.haak@hotmail.com", Civilite = "H", Nom = "Haak", Prenom = "Fay", DateNaissance = DateTime.Parse("1988-04-05")});

        }

        [TestMethod()]
        public void GetParticulier_ExistingIdPassed_ReturnsRightItem()
        {
            var mockRepository = new Mock<IRepository<Particulier>>();
            mockRepository.Setup(x => x.GetById(27)).Returns(testListe[0]);
            var userController = new ParticuliersController(mockRepository.Object);

            //Act
            var result = userController.GetParticulier(27);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Particulier>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Particulier>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Particulier), "Pas une Particulier");
            Assert.AreEqual(particulier, (Particulier)actionResult.Value, "Particulier pas identiques");
        }

        [TestMethod()]
        public void GetParticulier_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var mockRepository = new Mock<IRepository<Particulier>>();
            mockRepository.Setup(x => x.GetById(27)).Returns(testListe[0]);
            var userController = new ParticuliersController(mockRepository.Object);

            var result = userController.GetParticulier(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Particulier>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Particulier pas null");
        }

        [TestMethod()]
        public void GetParticuliers_ReturnsRightItems()
        {

            //Act
            var mockRepository = new Mock<IRepository<Particulier>>();
            mockRepository.Setup(x => x.GetAll()).Returns(testListe);
            var userController = new ParticuliersController(mockRepository.Object);

            var result = userController.GetParticuliers();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Particulier>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Particulier>> actionResult = result.Result as ActionResult<IEnumerable<Particulier>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.ProfilId <= 28 && s.ProfilId > 26).ToList(), "Pas les mêmes Particuliers");
        }

        [TestMethod()]
        public void PostParticulier_ModelValidated_CreationOK()
        {

            //Act
            var mockRepository = new Mock<IRepository<Particulier>>();
            mockRepository.Setup(x => x.GetById(27)).Returns(testListe[0]);
            var userController = new ParticuliersController(mockRepository.Object);

            var result = userController.PostParticulier(particulier).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Particulier>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Particulier), "Pas une Particulier");
            particulier.Email = ((Particulier)actionResult.Value).Email;
            Assert.AreEqual(particulier, (Particulier)actionResult.Value, "Particuliers pas identiques");

        }
        [TestMethod()]
        public void PostParticulier_CreationFailed()
        {
            var mockRepository = new Mock<IRepository<Particulier>>();
            mockRepository.Setup(x => x.GetById(27)).Returns(testListe[0]);
            var userController = new ParticuliersController(mockRepository.Object);

            //Act
            var result = userController.PostParticulier(particulier).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Particulier), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepository<Particulier>>();
            mockRepository.Setup(x => x.GetById(27)).Returns(testListe[0]);
            var userController = new ParticuliersController(mockRepository.Object);

            // Arrange
            int id = 2;//Mauvais ID

            // Act
            var result = await userController.PutParticulier(id, particulier);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {
            var mockRepository = new Mock<IRepository<Particulier>>();
            mockRepository.Setup(x => x.GetById(27)).Returns(testListe[0]);
            var userController = new ParticuliersController(mockRepository.Object);

            int id = 27; //BonID

            // Act
            var result = await userController.PutParticulier(id, particulier);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteParticulierTest()
        {
            var mockRepository = new Mock<IRepository<Particulier>>();
            mockRepository.Setup(x => x.GetById(27)).Returns(testListe[0]);
            var userController = new ParticuliersController(mockRepository.Object);

            //Act
            var resultDest = userController.DeleteParticulier(1);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<Particulier>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "Particulier pas null");
        }
    }
}