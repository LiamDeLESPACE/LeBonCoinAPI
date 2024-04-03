﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace LeBonCoinAPI.Controllers.Tests
{
    [TestClass()]
    public class ParticuliersControllerTests
    {
        private ParticuliersController _controller;

        private Mock<DataContext> _context;

        //Arrange
        Particulier particulier;
        List<Particulier> testListe;

        public ParticuliersControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=localhost; port=5432; Database=LeBonCoinSAE; uid=postgres; password=postgres;");
            _context = new Mock<DataContext>();
            _controller = new ParticuliersController(_context.Object);
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

            //Act
            var result = _controller.GetParticulier(27);

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
            var result = _controller.GetParticulier(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Particulier>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Particulier pas null");
        }

        [TestMethod()]
        public void GetParticuliers_ReturnsRightItems()
        {

            //Act
            var result = _controller.GetParticuliers();

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
            var result = _controller.PostParticulier(particulier).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Particulier>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Particulier), "Pas une Particulier");
            particulier.Email = ((Particulier)actionResult.Value).Email;
            Assert.AreEqual(particulier, (Particulier)actionResult.Value, "Particuliers pas identiques");

        }
        [TestMethod()]
        public void PostParticulier_CodeInsee_CreationFailed()
        {

            //Act
            var result = _controller.PostParticulier(particulier).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Particulier), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            int id = 2;//Mauvais ID

            // Act
            var result = await _controller.PutParticulier(id, particulier);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {

            int id = 27; //BonID

            // Act
            var result = await _controller.PutParticulier(id, particulier);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteParticulierTest()
        {

            //Act
            var result = _controller.GetParticulier(1);

            //Existence
            var actionResult = result.Result as ActionResult<Particulier>;
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Particulier), "Pas une Particulier");

            //Act
            var resultDest = _controller.DeleteParticulier(1);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<Particulier>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "Particulier pas null");
        }
    }
}