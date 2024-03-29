using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeBonCoinAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeBonCoinAPI.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Castle.Components.DictionaryAdapter.Xml;
using Moq;

namespace LeBonCoinAPI.Controllers.Tests
{
    [TestClass()]
    public class DepartementsControllerTests
    {
        private DepartementsController _controller;

        private DataContext _context;

        [TestInitialize]
        public void Initialize()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=localhost;Port=5432;Database=LeBonCoinSAE;Uid=postgres;Password=postgres;");
            _context = new DataContext(builder.Options);
        }

        [TestMethod()]
        public void DepartementsControllerTest()
        {
            //var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server = localhost; port = 5432; Database = LeBonCoinSAE; uid = postgres; password = postgres;");
            //_context = new DataContext(builder.Options);           
            //_controller = new DepartementsController(_context);
        }

        [TestMethod()]
        public void GetDepartementsTest()
        {
            // Mock du DbSet<Departement>
            var mockDbSet = new Mock<DbSet<Departement>>();
            // Configurer le contexte pour retourner le DbSet mocké
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(c => c.Departements).Returns(mockDbSet.Object);
            // Utiliser le contexte mocké dans le contrôleur
            _controller = new DepartementsController(mockContext.Object);

            // Arrange
            List<Departement> expected = new List<Departement>(); // Créer une liste vide car on utilise un contexte mocké

            // Act
            var result = _controller.GetDepartements().Result;

            // Assert
            CollectionAssert.AreEqual(expected, result.Value.ToList(), "Les listes ne sont pas identiques");



            /*// Mock du DbContext
            var mockContext = new Mock<DataContext>();
            // Mock du DbSet<Departement>
            var mockDbSet = new Mock<DbSet<Departement>>();
            // Configurer le contexte pour retourner le DbSet mocké
            mockContext.Setup(c => c.Departements).Returns(mockDbSet.Object);
            // Utiliser le contexte mocké dans le contrôleur
            _controller = new DepartementsController(mockContext.Object);

            // Arrange
            List<Departement> expected = _context.Departements.ToList();
            // Act
            var res = _controller.GetDepartements().Result;
            // Assert
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");

            */
            /*// Arrange 

            Departement dep1 = new Departement("1","Ain");         
            Departement dep2 = new Departement("2","Aisne");

            List<Departement> departementsEsperees = new List<Departement>();
            departementsEsperees.Add(dep1);
            departementsEsperees.Add(dep2);

            //var mockRepository = new Mock<IDataRepository<Departement>>();
            //mockRepository.Setup(r => r.()).Returns(departementsEsperees);

            //var userController = new DepartementsController((DataContext)mockRepository.Object);

            // Act
            var result = _controller.GetDepartements();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Departement>>), "Pas un ActionResult");
           // ActionResult<IEnumerable<Departement>> actionResult = result.Result as ActionResult<IEnumerable<Departement>>;
           // Assert.IsNotNull(actionResult, "ActionResult null");
           // Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            //CollectionAssert.AreEqual(departementsEsperees, actionResult.Value.Where(d => string.Compare(d.DepartementCode, "2") < 0).ToList(), "Pas les mêmes déartements");
            */
        }

        [TestMethod()]
        public void GetDepartementTest()
        {

        }

        [TestMethod()]
        public void PutDepartementTest()
        {

        }

        [TestMethod]
        public void Postdepartement_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Departement>>();
            var userController = new DepartementsController((DataContext)mockRepository.Object);

            Departement dep1 = new Departement("200", "Ainee");

            // Act
            var actionResult = userController.PostDepartement(dep1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Departement>), "Pas un ActionResult<Departement>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Departement), "Pas un Departement");
            dep1.DepartementCode = ((Departement)result.Value).DepartementCode;
            Assert.AreEqual(dep1, (Departement)result.Value, "Departements pas identiques");
        }

        [TestMethod()]
        public void DeleteDepartementTest()
        {

        }
    }
}