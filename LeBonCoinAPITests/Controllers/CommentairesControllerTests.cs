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
    public class CommentairesControllerTests
    {
        private CommentairesController _controller;
        private DataContext _context;
        private IRepositoryCommentaire<Commentaire> _dataRepository;

        //Arrange
        Commentaire commentaire;
        List<Commentaire> testListe;

        public CommentairesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=51.83.36.122; port=5432; Database=sa23; uid=sa23; password=idkY3t?; SearchPath=sae;");
            _context = new DataContext(builder.Options);
            _dataRepository = new CommentaireManager(_context);
            _controller = new CommentairesController(_dataRepository);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            commentaire = new Commentaire (43, 94, "Intéressé ! Pouvez-vous m\'envoyer plus de photos ?");

            testListe = new List<Commentaire>();
            testListe.Add(new Commentaire (43, 94, "Intéressé ! Pouvez-vous m\'envoyer plus de photos ?"));
            testListe.Add(new Commentaire (43, 26, "Je suis intéressé, mais pouvez-vous fournir plus de détails sur l\'état de l\'article ?"));

        }

        [TestMethod()]
        public void GetCommentaire_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var mockRepository = new Mock<IRepositoryCommentaire<Commentaire>>();
            mockRepository.Setup(x => x.GetByIds(43, 94)).Returns(testListe[0]);
            var userController = new DepartementsController(mockRepository.Object);

            var result = _controller.GetCommentaire(43, 94);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Commentaire>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Commentaire>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Commentaire), "Pas une Commentaire");
            Assert.AreEqual(commentaire, (Commentaire)actionResult.Value, "Commentaire pas identiques");
        }

        [TestMethod()]
        public void GetCommentaire_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var mockRepository = new Mock<IRepositoryCommentaire<Commentaire>>();
            mockRepository.Setup(x => x.GetByIds(43, 94)).Returns(testListe[0]);
            var userController = new DepartementsController(mockRepository.Object);

            var result = _controller.GetCommentaire(0,0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Commentaire>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Commentaire pas null");
        }

        [TestMethod()]
        public void GetCommentaires_ReturnsRightItems()
        {

            //Act
            var mockRepository = new Mock<IRepositoryCommentaire<Commentaire>>();
            mockRepository.Setup(x => x.GetAll()).Returns(testListe);
            var userController = new DepartementsController(mockRepository.Object);

            var result = _controller.GetCommentaires();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Commentaire>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Commentaire>> actionResult = result.Result as ActionResult<IEnumerable<Commentaire>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.ProfilId == 43).ToList(), "Pas les mêmes Commentaires");
        }

        [TestMethod()]
        public void PostCommentaire_ModelValidated_CreationOK()
        {
            var mockRepository = new Mock<IRepositoryCommentaire<Commentaire>>();
            mockRepository.Setup(x => x.GetByIds(43, 94)).Returns(testListe[0]);
            var userController = new DepartementsController(mockRepository.Object);

            //Act
            var result = _controller.PostCommentaire(commentaire).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Commentaire>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Commentaire), "Pas une Commentaire");
            commentaire.Contenu = ((Commentaire)actionResult.Value).Contenu;
            Assert.AreEqual(commentaire, (Commentaire)actionResult.Value, "Commentaires pas identiques");

        }
        [TestMethod()]
        public void PostCommentaire_CreationFailed()
        {
            var mockRepository = new Mock<IRepositoryCommentaire<Commentaire>>();
            mockRepository.Setup(x => x.GetByIds(43, 94)).Returns(testListe[0]);
            var userController = new DepartementsController(mockRepository.Object);


            //Act
            var result = _controller.PostCommentaire(commentaire).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Commentaire), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepositoryCommentaire<Commentaire>>();
            mockRepository.Setup(x => x.GetByIds(43, 94)).Returns(testListe[0]);
            var userController = new DepartementsController(mockRepository.Object);

            // Arrange
            int id1 = 2; int id2 = 2;//Mauvais ID

            // Act
            var result = await _controller.PutCommentaire(id1, id2, commentaire);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {
            var mockRepository = new Mock<IRepositoryCommentaire<Commentaire>>();
            mockRepository.Setup(x => x.GetByIds(43, 94)).Returns(testListe[0]);
            var userController = new DepartementsController(mockRepository.Object);

            int id1 = 43; int id2 = 94; //BonID

            // Act
            var result = await _controller.PutCommentaire(id1, id2, commentaire);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteCommentaireTest()
        {
            var mockRepository = new Mock<IRepositoryCommentaire<Commentaire>>();
            mockRepository.Setup(x => x.GetByIds(43, 94)).Returns(testListe[0]);
            var userController = new DepartementsController(mockRepository.Object);

            //Act
            var resultDest = _controller.DeleteCommentaire(43, 94);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<Commentaire>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "Commentaire pas null");
        }
    }
}