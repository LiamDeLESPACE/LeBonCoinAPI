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

namespace LeBonCoinAPI.Controllers.Tests
{
    [TestClass()]
    public class AdressesControllerTests
    {
        private AdressesController _controller;

        private DataContext _context;

        /*var testListe = new List<Adresse>();
            testListe.Add(new Adresse { CodeInsee = "74002", Rue = "Chemin de Cret-Vial", Numero = 47 });
            testListe.Add(new Adresse { CodeInsee = "74002", Rue = "Rue de la capetaz", Numero = 1 });
            testListe.Add(new Adresse { CodeInsee = "74002", Rue = "Chemin de Cret-Vial", Numero = 47 });
            testListe.Add(new Adresse { CodeInsee = "01431", Rue = "Rue de Cornier", Numero = 15 });*/

        public AdressesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=localhost; port=5432; Database=LeBonCoinSAE; uid=postgres; password=postgres;");
            _context = new DataContext(builder.Options);
            _controller = new AdressesController(_context);
        }

        [TestMethod()]
        public void GetAdresse_ExistingIdPassed_ReturnsRightItem()
        {
            //Arrange
            Adresse adresse = new Adresse
            {
                CodeInsee = "01431",
                Rue = "Rue de Cornier",
                Numero = 15
            };

            //Act
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
            var result = _controller.GetAdresse(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Adresse>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Adresse pas null");
        }

        [TestMethod()]
        public void GetAdresses_ReturnsRightItems()
        {
            //Arrange
            List<Adresse> testListe = new List<Adresse>();
            testListe.Add(new Adresse { CodeInsee = "74002", Rue = "Chemin de Cret-Vial", Numero = 47 });
            testListe.Add(new Adresse { CodeInsee = "74002", Rue = "Rue de la capetaz", Numero = 1 });

            //Act
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
            Adresse adresse1 = new Adresse
            {
                CodeInsee = "01431",
                Rue = "Rue de Cornier",
                Numero = 15

            };

            //Act
            var result = _controller.PostAdresse(adresse1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Adresse>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Adresse), "Pas une adresse");
            adresse1.CodeInsee = ((Adresse)actionResult.Value).CodeInsee;
            Assert.AreEqual(adresse1, (Adresse)actionResult.Value, "Adresses pas identiques");

        }
        [TestMethod()]
        public void PostAdresse_CodeInsee_CreationFailed()
        {
            //Arrange
            Adresse adresse1 = new Adresse
            {
                CodeInsee = "01431",
                Rue = "Rue de Cornier",
                Numero = 15

            };

            //Act
            var result = _controller.PostAdresse(adresse1).Result;
        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            int idVoulu = 1;
            Adresse adresse2 = new Adresse 
            { 
                AdresseId = 2, 
                CodeInsee = "01431", 
                Rue = "Rue de Cornier", 
                Numero = 15

            };

            // Act
            var result = await _controller.PutAdresse(idVoulu, adresse2);

            // Assert
        }

        /*[TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var mockContext = new Mock<YourDbContext>();
            var controller = new AdresseController(mockContext.Object);
            int id = 1;
            var adresse = new Adresse { AdresseId = 1 }; // Suppose une adresse avec ID correspondant

            // Act
            var result = await controller.Put(id, adresse);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [TestMethod()]
        public async Task Put_WithConcurrencyException_ReturnsNotFound()
        {
            // Arrange
            var mockContext = new Mock<YourDbContext>();
            mockContext.Setup(c => c.SaveChangesAsync()).Throws(new DbUpdateConcurrencyException());
            var controller = new AdresseController(mockContext.Object);
            int id = 1;
            var adresse = new Adresse { AdresseId = 1 }; // Suppose une adresse avec ID correspondant

            // Act
            var result = await controller.Put(id, adresse);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }*/

        [TestMethod()]
        public void PutAdresseTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteAdresseTest()
        {

        }
    }
}