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
using System.Drawing;

namespace LeBonCoinAPI.Controllers.Tests
{
    [TestClass()]
    public class PhotosControllerTests
    {
        private PhotosController _controller;

        private Mock<DataContext> _context;

        //Arrange
        Photo photoProfil;
        List<Photo> testListeAnnonce;

        public PhotosControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=localhost; port=5432; Database=LeBonCoinSAE; uid=postgres; password=postgres;");
            _context = new Mock<DataContext>();
            _controller = new PhotosController(_context.Object);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            photoProfil = new Photo { PhotoId = 151, ProfilId=1, AnnonceId=null, URL= "photo (151)" };

            testListeAnnonce = new List<Photo>();
            testListeAnnonce.Add(new Photo { PhotoId = 1, ProfilId = null, AnnonceId = 1, URL = "photo (1)" });
            testListeAnnonce.Add(new Photo { PhotoId = 2, ProfilId = null, AnnonceId = 1, URL = "photo (2)" });

        }

        [TestMethod()]
        public void GetPhotosAnnonce_ReturnsRightItems()
        {

            //Act
            var result = _controller.GetPhotosAnnonce(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Photo>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Photo>> actionResult = result.Result as ActionResult<IEnumerable<Photo>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListeAnnonce, actionResult.Value.Where(s => s.AnnonceId == 1).ToList(), "Pas les mêmes Photos");
        }
        [TestMethod()]
        public void GetPhotoProfil_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var result = _controller.GetPhotoProfil(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Photo>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Photo>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Photo), "Pas une Photo");
            Assert.AreEqual(photoProfil, (Photo)actionResult.Value, "Photo pas identiques");
        }
        [TestMethod()]
        public void GetPhoto_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var result = _controller.GetPhotoProfil(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Photo>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Photo pas null");
        }
        //Pas sur de la pertinence de ce test, si vous pensez que c'est le cas décommentez mais faites attention aux ID
        /*[TestMethod()]
        public void GetPhotos_ReturnsRightItems()
        {

            //Act
            var result = _controller.GetPhotos();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Photo>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Photo>> actionResult = result.Result as ActionResult<IEnumerable<Photo>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.PhotoId <= 2).ToList(), "Pas les mêmes Photos");
        }*/

        [TestMethod()]
        public void PostPhoto_ModelValidated_CreationOK()
        {

            //Act
            var result = _controller.PostPhoto(photoProfil).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Photo>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Photo), "Pas une Photo");
            photoProfil.URL = ((Photo)actionResult.Value).URL;
            Assert.AreEqual(photoProfil, (Photo)actionResult.Value, "Photos pas identiques");

        }
        [TestMethod()]
        public void PostPhoto_CodeInsee_CreationFailed()
        {

            //Act
            var result = _controller.PostPhoto(photoProfil).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Photo), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            int id = 2;//Mauvais ID

            // Act
            var result = await _controller.PutPhoto(id, photoProfil);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {

            int id = 151; //BonID

            // Act
            var result = await _controller.PutPhoto(id, photoProfil);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeletePhotoTest()
        {

            //Act
            var result = _controller.GetPhotoP(151);

            //Existence
            var actionResult = result.Result as ActionResult<Photo>;
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Photo), "Pas une Photo");

            //Act
            var resultDest = _controller.DeletePhoto(151);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<Photo>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "Photo pas null");
        }
    }
}