﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class VillesControllerTests
    {
        private VillesController _controller;
        private DataContext _context;
        private IRepositoryVille<Ville> _dataRepository;

        //Arrange
        Ville ville;
        Ville villeUpdated;
        List<Ville> testListe;

        public VillesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=51.83.36.122; port=5432; Database=sa23; uid=sa23; password=idkY3t?; SearchPath=sae;");
            _context = new DataContext(builder.Options);
            _dataRepository = new VilleManager(_context);
            _controller = new VillesController(_dataRepository);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            ville = new Ville { CodeInsee="01004", DepartementCode= "1", Nom = "AMBERIEU EN BUGEY", CodePostal="01500" };
            villeUpdated = new Ville { CodeInsee = "01004", DepartementCode = "12", Nom = "AMBERIEUU EN BUGEY", CodePostal = "01501" };

            testListe = new List<Ville>();
            testListe.Add(ville);
            testListe.Add(new Ville { CodeInsee = "01005", DepartementCode = "1", Nom = "AMBERIEUX EN DOMBES", CodePostal = "01330" });

        }

        [TestMethod()]
        public void GetVille_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var mockRepository = new Mock<IRepositoryVille<Ville>>();
            mockRepository.Setup(x => x.GetByInsee("01004").Result).Returns(testListe[0]);
            var userController = new VillesController(mockRepository.Object);

            var result = userController.GetVilleByInsee("01004");

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Ville>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Ville>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Ville), "Pas une Ville");
            Assert.AreEqual(ville, (Ville)actionResult.Value, "Ville pas identiques");
        }

        [TestMethod()]
        public void GetVille_UnknownIdPassed_ReturnsNotFoundResult()
        {
            var mockRepository = new Mock<IRepositoryVille<Ville>>();
            mockRepository.Setup(x => x.GetByInsee("01004").Result).Returns(testListe[0]);
            var userController = new VillesController(mockRepository.Object);

            //Act
            var result = userController.GetVilleByInsee("00000");

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Ville>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Ville pas null");
        }

        [TestMethod()]
        public void GetVilles_ReturnsRightItems()
        {
            var mockRepository = new Mock<IRepositoryVille<Ville>>();
            mockRepository.Setup(x => x.GetAll().Result).Returns(testListe);
            var userController = new VillesController(mockRepository.Object);

            //Act
            var result = userController.GetVilles();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Ville>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Ville>> actionResult = result.Result as ActionResult<IEnumerable<Ville>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.CodeInsee == "01004" || s.CodeInsee == "01005").ToList(), "Pas les mêmes Villes");
        }

        [TestMethod()]
        public void PostVille_ModelValidated_CreationOK()
        {
            var mockRepository = new Mock<IRepositoryVille<Ville>>();
            //mockRepository.Setup(x => x.GetByInsee("01004").Result).Returns(testListe[0]);
            var userController = new VillesController(mockRepository.Object);

            //Act
            var result = userController.PostVille(ville).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Ville>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Ville), "Pas une Ville");
            ville.Nom = ((Ville)actionResult.Value).Nom;
            Assert.AreEqual(ville, (Ville)actionResult.Value, "Villes pas identiques");

        }
        [TestMethod()]
        public void PostVille_CreationFailed()
        {
            var mockRepository = new Mock<IRepositoryVille<Ville>>();
            //mockRepository.Setup(x => x.GetByInsee("01004").Result).Returns(testListe[0]);
            var userController = new VillesController(mockRepository.Object);

            //Act
            var result = userController.PostVille(ville).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Ville), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public void Put_WithInvalidId_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepositoryVille<Ville>>();
            mockRepository.Setup(x => x.GetByInsee("01004").Result).Returns(ville);
            var userController = new VillesController(mockRepository.Object);

      
            

            // Act
            var result = userController.PutVille("00000", villeUpdated);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult), "Pas un BadRequestResult");
        }

        [TestMethod()]
        public void Put_WithValidId_ReturnsNoContent()
        {
            var mockRepository = new Mock<IRepositoryVille<Ville>>();
            mockRepository.Setup(x => x.GetByInsee("01004").Result).Returns(ville);
            var userController = new VillesController(mockRepository.Object);

            

            // Act
            var result = userController.PutVille("01004", villeUpdated);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod()]
        public void DeleteVilleTest()
        {
            var mockRepository = new Mock<IRepositoryVille<Ville>>();
            mockRepository.Setup(x => x.GetByInsee("01004").Result).Returns(testListe[0]);
            var userController = new VillesController(mockRepository.Object);

            //Act
            var resultDest = userController.DeleteVille("01004");

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(NoContentResult), "Pas un ActionResult");
            
        }
    }
}