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



        [TestMethod()]
        public void DepartementsControllerTest()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server = localhost; port = 5432; Database = LeBonCoinSAE; uid = postgres; password = postgres;");
            _context = new DataContext(builder.Options);
            _controller = new DepartementsController(_context);
        }

        [TestMethod()]
        public void GetDepartementsTest()
        {
            // Arrange 

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
            //var mockRepository = new Mock<IDataRepository<Departement>>();
            //var userController = new DepartementsController((DataContext)mockRepository.Object);

            Departement dep1 = new Departement("200", "Ainee");

            // Act
            var actionResult = _controller.PostDepartement(dep1).Result;

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