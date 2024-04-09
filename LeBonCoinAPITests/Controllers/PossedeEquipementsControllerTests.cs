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
    public class PossedeEquipementControllerTests
    {
        private PossedeEquipementsController _controller;
        private DataContext _context;
        private IRepositoryPossedeEquipement<PossedeEquipement> _dataRepository;

        //Arrange
        PossedeEquipement possedeEquipement;
        List<PossedeEquipement> testListe;

        public PossedeEquipementControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=51.83.36.122; port=5432; Database=sa23; uid=sa23; password=idkY3t?; SearchPath=sae;");
            _context = new DataContext(builder.Options);
            _dataRepository = new PossedeEquipementManager(_context);
            _controller = new PossedeEquipementsController(_dataRepository);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            possedeEquipement = new PossedeEquipement { AnnonceId=43, EquipementId=17 };

            testListe = new List<PossedeEquipement>();
            testListe.Add(new PossedeEquipement { AnnonceId = 43, EquipementId = 17 });
            testListe.Add(new PossedeEquipement { AnnonceId = 17, EquipementId = 17 });

        }

        [TestMethod()]
        public void GetPossedeEquipement_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var mockRepository = new Mock<IRepositoryPossedeEquipement<PossedeEquipement>>();
            mockRepository.Setup(x => x.GetByIds(43,17).Result).Returns(testListe[0]);
            var userController = new PossedeEquipementsController(mockRepository.Object);

            var result = userController.GetPossedeEquipementByIds(43, 17);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<PossedeEquipement>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<PossedeEquipement>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(PossedeEquipement), "Pas une PossedeEquipement");
            Assert.AreEqual(possedeEquipement, (PossedeEquipement)actionResult.Value, "PossedeEquipement pas identiques");
        }

        [TestMethod()]
        public void GetPossedeEquipement_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var mockRepository = new Mock<IRepositoryPossedeEquipement<PossedeEquipement>>();
            mockRepository.Setup(x => x.GetByIds(43, 17).Result).Returns(testListe[0]);
            var userController = new PossedeEquipementsController(mockRepository.Object);

            var result = userController.GetPossedeEquipementByIdAnnonce(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<PossedeEquipement>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "PossedeEquipement pas null");
        }

        [TestMethod()]
        public void GetPossedeEquipement_ReturnsRightItems()
        {

            //Act
            var mockRepository = new Mock<IRepositoryPossedeEquipement<PossedeEquipement>>();
            mockRepository.Setup(x => x.GetAll().Result).Returns(testListe);
            var userController = new PossedeEquipementsController(mockRepository.Object);

            var result = userController.GetPossedeEquipements();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<PossedeEquipement>>), "Pas un ActionResult");
            ActionResult<IEnumerable<PossedeEquipement>> actionResult = result.Result as ActionResult<IEnumerable<PossedeEquipement>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.EquipementId== 17).ToList(), "Pas les mêmes PossedeEquipement");
        }

        [TestMethod()]
        public void PostPossedeEquipement_ModelValidated_CreationOK()
        {

            //Act
            var mockRepository = new Mock<IRepositoryPossedeEquipement<PossedeEquipement>>();
            //mockRepository.Setup(x => x.GetByIds(43, 17).Result).Returns(testListe[0]);
            var userController = new PossedeEquipementsController(mockRepository.Object);

            var result = userController.PostPossedeEquipement(possedeEquipement).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<PossedeEquipement>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(PossedeEquipement), "Pas une PossedeEquipement");
            possedeEquipement.EquipementId = ((PossedeEquipement)actionResult.Value).EquipementId;
            Assert.AreEqual(possedeEquipement, (PossedeEquipement)actionResult.Value, "PossedeEquipement pas identiques");

        }
        [TestMethod()]
        public void PostPossedeEquipement_CreationFailed()
        {
            var mockRepository = new Mock<IRepositoryPossedeEquipement<PossedeEquipement>>();
            //mockRepository.Setup(x => x.GetByIds(43, 17).Result).Returns(testListe[0]);
            var userController = new PossedeEquipementsController(mockRepository.Object);

            //Act
            var result = userController.PostPossedeEquipement(possedeEquipement).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(PossedeEquipement), "Pas un CreatedAtActionResult");

        }

        /*[TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepositoryPossedeEquipement<PossedeEquipement>>();
            mockRepository.Setup(x => x.GetByIds(43, 17).Result).Returns(testListe[0]);
            var userController = new PossedeEquipementsController(mockRepository.Object);

            // Arrange
            int id1 = 2; int id2 = 2;//Mauvais ID

            // Act
            var result = await userController.PutPossedeEquipement(id1, id2, possedeEquipement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {
            var mockRepository = new Mock<IRepositoryPossedeEquipement<PossedeEquipement>>();
            mockRepository.Setup(x => x.GetByIds(43, 17).Result).Returns(testListe[0]);
            var userController = new PossedeEquipementsController(mockRepository.Object);

            int id1 = 43; int id2 = 17; //BonID

            // Act
            var result = await userController.PutPossedeEquipement(id1, id2, possedeEquipement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }*/

        [TestMethod()]
        public void DeletePossedeEquipementTest()
        {

            var mockRepository = new Mock<IRepositoryPossedeEquipement<PossedeEquipement>>();
            mockRepository.Setup(x => x.GetByIds(43, 17).Result).Returns(testListe[0]);
            var userController = new PossedeEquipementsController(mockRepository.Object);

            //Act
            var resultDest = userController.DeletePossedeEquipement(43, 17);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(NoContentResult), "Pas un NoContentResult");
        }
    }
}