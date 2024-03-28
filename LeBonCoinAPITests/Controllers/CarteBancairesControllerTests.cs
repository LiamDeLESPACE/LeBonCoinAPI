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
using Microsoft.Extensions.DependencyInjection;

namespace LeBonCoinAPI.Controllers.Tests
{
    [TestClass()]
    public class CarteBancairesControllerTests
    {
        private CarteBancairesController _controller;

        private DataContext _context;

        [TestMethod()]
        public void CarteBancairesControllerTest()
        {
            /*var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server = localhost; port = 5432; Database = LeBonCoinSAE; uid = postgres; password = postgres;");
            _context = new DataContext();
            _controller = new CarteBancairesController(_context);*/

        }

        [TestMethod()]
        public void GetCarteBancairesTest()
        {

        }

        [TestMethod()]
        public void GetCarteBancaireTest()
        {

        }

        [TestMethod()]
        public void PutCarteBancaireTest()
        {

        }

        [TestMethod()]
        public void PostCarteBancaire_ModelValidated_CreationOK()
        {
            // Arrange
            CarteBancaire cb1 = new CarteBancaire(20, "4903422675465567");


            // Act
            var result = _controller.PostCarteBancaire(cb1).Result; // Result pour appeler la méthode async de manière synchrone, afin d'obtenir le résultat

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<CarteBancaire>), "Pas un ActionResult<CarteBancaire>");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(CarteBancaire), "Pas une CarteBancaire");
            cb1.CarteId = ((CarteBancaire)actionResult.Value).CarteId;
            Assert.AreEqual(cb1, (CarteBancaire)actionResult.Value, "CarteBancaires pas identiques");
        }

        [TestMethod()]
        public void DeleteCarteBancaireTest()
        {

        }
    }
}