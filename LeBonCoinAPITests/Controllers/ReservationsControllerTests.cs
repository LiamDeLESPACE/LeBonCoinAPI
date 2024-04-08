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
    public class ReservationsControllerTests
    {
        private ReservationsController _controller;
        private DataContext _context;
        private IRepositoryReservation<Reservation> _dataRepository;

        //Arrange
        Reservation reservation;
        List<Reservation> testListe;

        public ReservationsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=51.83.36.122; port=5432; Database=sa23; uid=sa23; password=idkY3t?; SearchPath=sae;");
            _context = new DataContext(builder.Options);
            _dataRepository = new ReservationManager(_context);
            _controller = new ReservationsController(_dataRepository);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            reservation = new Reservation { ReservationId=1, AnnonceId=46, ProfilId=75, DateArrivee=DateTime.Parse("2021-12-01"), DateDepart = DateTime.Parse("2021-12-11"), NombreVoyageur = 9, Nom="Fabre", Prenom="Dara", Telephone="0616708214" };

            testListe = new List<Reservation>();
            testListe.Add(reservation);
            testListe.Add(new Reservation { ReservationId = 2, AnnonceId = 23, ProfilId = 67, DateArrivee = DateTime.Parse("2024-01-06"), DateDepart = DateTime.Parse("2024-01-20"), NombreVoyageur = 5, Nom = "Peeters", Prenom = "Judah", Telephone = "0797981221" });
        }

        [TestMethod()]
        public void GetReservation_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var mockRepository = new Mock<IRepositoryReservation<Reservation>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new ReservationsController(mockRepository.Object);

            var result = _controller.GetReservation(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Reservation>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Reservation>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Reservation), "Pas une Reservation");
            Assert.AreEqual(reservation, (Reservation)actionResult.Value, "Reservation pas identiques");
        }

        [TestMethod()]
        public void GetReservation_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var mockRepository = new Mock<IRepositoryReservation<Reservation>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new ReservationsController(mockRepository.Object);

            var result = _controller.GetReservation(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Reservation>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Reservation pas null");
        }

        [TestMethod()]
        public void GetReservations_ReturnsRightItems()
        {
            var mockRepository = new Mock<IRepositoryReservation<Reservation>>();
            mockRepository.Setup(x => x.GetAll()).Returns(testListe[0]);
            var userController = new ReservationsController(mockRepository.Object);

            //Act
            var result = _controller.GetReservations();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Reservation>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Reservation>> actionResult = result.Result as ActionResult<IEnumerable<Reservation>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.AnnonceId <=2).ToList(), "Pas les mêmes Reservations");
        }

        [TestMethod()]
        public void PostReservation_ModelValidated_CreationOK()
        {
            var mockRepository = new Mock<IRepositoryReservation<Reservation>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new ReservationsController(mockRepository.Object);

            //Act
            var result = _controller.PostReservation(reservation).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Reservation>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Reservation), "Pas une Reservation");
            reservation.Telephone = ((Reservation)actionResult.Value).Telephone;
            Assert.AreEqual(reservation, (Reservation)actionResult.Value, "Reservations pas identiques");

        }
        [TestMethod()]
        public void PostReservation_CreationFailed()
        {
            var mockRepository = new Mock<IRepositoryReservation<Reservation>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new ReservationsController(mockRepository.Object);

            //Act
            var result = _controller.PostReservation(reservation).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Reservation), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepositoryReservation<Reservation>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new ReservationsController(mockRepository.Object);

            // Arrange
            int id = 37;//Mauvais ID

            // Act
            var result = await _controller.PutReservation(id, reservation);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {
            var mockRepository = new Mock<IRepositoryReservation<Reservation>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new ReservationsController(mockRepository.Object);

            int id = 1; //BonID

            // Act
            var result = await _controller.PutReservation(id, reservation);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteReservationTest()
        {
            var mockRepository = new Mock<IRepositoryReservation<Reservation>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new ReservationsController(mockRepository.Object);

            //Act
            var resultDest = _controller.DeleteReservation(1);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<Reservation>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "Reservation pas null");
        }
    }
}