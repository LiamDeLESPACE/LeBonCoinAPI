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
    public class AnnoncesControllerTests
    {
        private AnnoncesController _controller;
        private DataContext _context;
        private IRepositoryAnnonce<Annonce> _dataRepository;

        //Arrange
        Annonce annonce;
        List<Annonce> testListe;

        public AnnoncesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseNpgsql("Server=51.83.36.122; port=5432; Database=sa23; uid=sa23; password=idkY3t?; SearchPath=sae;");
            _context = new DataContext(builder.Options);
            _dataRepository = new AnnonceManager(_context);
            _controller = new AnnoncesController(_dataRepository);
        }

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Rajouter les initialisations exécutées avant chaque test
            annonce = new Annonce { AnnonceId = 1, AdresseId = 51, TypeLogementId = 9, ProfilId = 53, Titre = "Maison de vacances 10 pers piscine, spa", DureeMinimumSejour = 1, Active = true, DatePublication = DateTime.Parse("2021-10-03"), Description = "A Angles sur Anglin, en bordure des champs, ce gîte de 220 m2, situé dans un ancien corps de ferme, pour 6 à 10 pers a été réhabilité de manière contemporaine. Piscine extérieure, spa et sauna, terrain de boules, ping pong, baby foot, trampoline, vélos à partager avec nos 3 autres gîtes. Notre gîte est composé d\'une grande pièce de vie très lumineuse avec une cuisine américaine, un poêle, de 2 chambres lit de 1m60 avec salles de douches privées, wc et à l\'étage de 2 chambres avec chacune 2 lits de 0m90 une salle de bain et une chambre avec lit de 1m60 avec sa salle de douche privée. terrasse, barbecue et jardin privés. Ce gîte peut être associé à nos 3 autres gîtes, capacité totale 28 personnes Pour une location sur un week end vous pouvez profiter du gîte jusqu\'à 18h Piscine de 10m sur 4m50 extérieure à partager avec nos autres gîtes. Espace spa et sauna (10 euros/h pour 2 pers) sur réservation Envie d\'un massage, Joseph pratique le massage ayurvédique et se déplace dans votre gîte ou dans l\'espace détente. Prix à la semaine entre 900 et 1350 euros. Prix pour 2 nuits week end 600 euros forfait ménage possible 60 euros Site internet giteslaligne.fr", Etoile = 3 , NombrePersonnesMax = 11 , PrixParNuit = 185 , NombreChambres = 5 };

            testListe = new List<Annonce>();
            testListe.Add(new Annonce { AnnonceId = 1, AdresseId = 51, TypeLogementId = 9, ProfilId = 53, Titre = "Maison de vacances 10 pers piscine, spa", DureeMinimumSejour = 1, Active = true, DatePublication = DateTime.Parse("2021-10-03"), Description = "A Angles sur Anglin, en bordure des champs, ce gîte de 220 m2, situé dans un ancien corps de ferme, pour 6 à 10 pers a été réhabilité de manière contemporaine. Piscine extérieure, spa et sauna, terrain de boules, ping pong, baby foot, trampoline, vélos à partager avec nos 3 autres gîtes. Notre gîte est composé d\'une grande pièce de vie très lumineuse avec une cuisine américaine, un poêle, de 2 chambres lit de 1m60 avec salles de douches privées, wc et à l\'étage de 2 chambres avec chacune 2 lits de 0m90 une salle de bain et une chambre avec lit de 1m60 avec sa salle de douche privée. terrasse, barbecue et jardin privés. Ce gîte peut être associé à nos 3 autres gîtes, capacité totale 28 personnes Pour une location sur un week end vous pouvez profiter du gîte jusqu\'à 18h Piscine de 10m sur 4m50 extérieure à partager avec nos autres gîtes. Espace spa et sauna (10 euros/h pour 2 pers) sur réservation Envie d\'un massage, Joseph pratique le massage ayurvédique et se déplace dans votre gîte ou dans l\'espace détente. Prix à la semaine entre 900 et 1350 euros. Prix pour 2 nuits week end 600 euros forfait ménage possible 60 euros Site internet giteslaligne.fr", Etoile = 3, NombrePersonnesMax = 11, PrixParNuit = 185, NombreChambres = 5 });
            testListe.Add(new Annonce { AnnonceId= 2, AdresseId = 52, TypeLogementId = 10, ProfilId = 38, Titre = "Location gîte Aux Portes de Bellevue à SALBRIS - SOLOGNE/CENTRE VAL DE LOIRE", DureeMinimumSejour = 9 , Active = true, DatePublication = DateTime.Parse("2021-07-15"), Description = "Nous vous proposons à la location une maison de plain-pied, qui sera entièrement rénovée mi-octobre 2023 (les photos seront disponibles au fur et à mesure), située à SALBRIS entre BOURGES et ORLEANS dans quartier très calme. Ville desservie par gare SNCF et Autoroutes A71, A20 et A85 Comprenant : 1 chambre 1 lit double - 1 seconde 1 lit double ou 2 lits enfants suivant besoin - 1 WC séparé - 1 salle de douche avec machine à laver, étendoir à linge, sèche serviette, sèche-cheveux - 1 cuisine ouverte équipée : Combiné réfrigérateur/congélateur, four électrique, plaques gaz, hotte, micro-ondes, cafetière, vaisselle, plancha à gaz et petit électroménager. La pièce de vie « salle à manger et coin salon avec TV » - WIFI Lits faits à votre arrivée, vous pouvez nous réserver un trousseau de toilette (2 serviettes + 2 gants + 2 tapis de bain : 15€/2personnes) Climatisation manuelle – Chauffage électrique et ajout d’un poêle à granulés d’ici la fin de l’année. Nécessaire à l’entretien (Aspirateur, pelle, balayette, balai, bassine, seau, serpillère, brosse, torchons, produits « sol, WC, vitres, cuisine », papier toilette, savon, éponge, insecticide, pastille lave vaisselle, lessive machine à laver...) / Epicerie et fournitures ménagère de premières nécessités. Animaux (à voir au moment de la réservation) Terrain clôturé, terrasse semi-ombragée avec salon de jardin, coure privée avec emplacement pour une voiture et mini basse-cour pour les enfants. Services, visites et loisirs : Accessible à pied depuis le gîte : - L’étang de Bellevue, du Camping et la rivière la SAULDRE (à 100m à pied) et l’ALMERIA PARC (à 500 m à pied), pour nos passionnés de pêche et chevaux. Entre 5mn et 10mn à pied - SUPER U - Les restaurants Le Parc, les Oliviers et les Copains d’Abord (Soirées concerts) - Un complexe sportif (Rugby, tennis, foot et la piscine municipale « La Salamandre » restaurée en 2022. Le Parc Albert Benoit avec : Sa Guinguette, concerts - son boulodrome - ses vide-greniers - son super STOCK CAR et feu d’artifice du 15 Août. A proximité en voiture : Centre-ville tous commerces, le marché les jeudis matin et samedis matin, supermarché CARREFOUR, restaurants... Direction ROMORANTIN-LANTHENAY descente et promenade sur la rivière LA SAULDRE en canoë-kayak au départ du CRJS (10mn) CENTER PARC CHAUMONT SUR THARONNE (20mn) Centre équestre FFE et grande fête de la chasse en Juin « GAME FAIR » LAMOTTE-BEUVRON (20mn) Village médiéval MENNETOU SUR CHER (20mn) Le Circuit international de karting de SALBRIS à 7km Pôle des étoiles NANCAY « Observatoire et 4ème plus grand radiotélescope du monde » (20 mn) SALBRIS se trouve sur la route de Jacques Cœur : AUBIGNY SUR NERE « la Cité des Stuarts » et sa traditionnelle fête écossaise en juillet (30mn) / BOURGES et son festival de musique en avril \"le printemps de BOURGES\" (40mn) Zoo de BEAUVAL SAINT-AIGNAN (1h00) A 1h00, la route des Châteaux de la Loire : VALENCAY, CHAMBORD, CHEVERNY, CHENONCEAU, AMBOISE, CHAUMONT SUR LOIRE La route des vignobles : SANCERRE, MENNETOU SALON.., LA BORNE village des potiers (1h00) Le LAC D’EGUZON 312 Ha (1h30) Parc Floral ORLEANS (45mn) et APREMON SUR ALLIER (1h30), sans oublier le détour par la ville de TOURS Les tarifs : 1 nuit pour 4 personnes 100€/1 nuit pour 3 personnes 90€/1 nuit pour 2 personnes 80 € Forfait à la semaine : 650€ pour 4 personnes 600€ pour 3 personnes 550€ pour 2 personnes Réservation 2 nuits minimum. Une attestation d’assurance responsabilité civile villégiature sera demandée à la signature du contrat. Le ménage complet devra être effectué avant le départ ou choix d\\'un forfait ménage sur demande : 15€ par jour ou 80€ à la semaine. Autres renseignements : Nous contacter via la messagerie LEBONCOIN.", Etoile = 2 , NombrePersonnesMax = 7 , PrixParNuit = 202, NombreChambres = 3 });

        }

        [TestMethod()]
        public void GetAnnonce_ExistingIdPassed_ReturnsRightItem()
        {

            //Act
            var mockRepository = new Mock<IRepositoryAnnonce<Annonce>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new AnnoncesController(mockRepository.Object);

            var result = userController.GetAnnonce(1);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Annonce>), "Pas un ActionResult");

            var actionResult = result.Result as ActionResult<Annonce>;

            //Assert
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            Assert.IsInstanceOfType(actionResult.Value, typeof(Annonce), "Pas une Annonce");
            Assert.AreEqual(annonce, (Annonce)actionResult.Value, "Annonce pas identiques");
        }

        [TestMethod()]
        public void GetAnnonce_UnknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var mockRepository = new Mock<IRepositoryAnnonce<Annonce>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new AnnoncesController(mockRepository.Object);

            var result = userController.GetAnnonce(0);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Annonce>), "Pas un ActionResult");
            Assert.IsNull(result.Result.Value, "Annonce pas null");
        }

        [TestMethod()]
        public void GetAnnonces_ReturnsRightItems()
        {
            var mockRepository = new Mock<IRepositoryAnnonce<Annonce>>();
            mockRepository.Setup(x => x.GetAll()).Returns(testListe);
            var userController = new AnnoncesController(mockRepository.Object);

            //Act
            var result = userController.GetAnnonces();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<Annonce>>), "Pas un ActionResult");
            ActionResult<IEnumerable<Annonce>> actionResult = result.Result as ActionResult<IEnumerable<Annonce>>;
            Assert.IsNotNull(actionResult, "ActionResult null");
            Assert.IsNotNull(actionResult.Value, "Valeur nulle");
            CollectionAssert.AreEqual(testListe, actionResult.Value.Where(s => s.AnnonceId <= 2).ToList(), "Pas les mêmes Annonces");
        }

        [TestMethod()]
        public void PostAnnonce_ModelValidated_CreationOK()
        {
            var mockRepository = new Mock<IRepositoryAnnonce<Annonce>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new AnnoncesController(mockRepository.Object);

            //Act
            var result = userController.PostAnnonce(annonce).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Annonce>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(actionResult.Value, typeof(Annonce), "Pas une Annonce");
            annonce.Description = ((Annonce)actionResult.Value).Description;
            Assert.AreEqual(annonce, (Annonce)actionResult.Value, "Annonces pas identiques");

        }
        [TestMethod()]
        public void PostAnnonce_CreationFailed()
        {
            var mockRepository = new Mock<IRepositoryAnnonce<Annonce>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new AnnoncesController(mockRepository.Object);

            //Act
            var result = userController.PostAnnonce(annonce).Result;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(Annonce), "Pas un CreatedAtActionResult");

        }

        [TestMethod()]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepositoryAnnonce<Annonce>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new AnnoncesController(mockRepository.Object);

            // Arrange
            int id = 2;//Mauvais ID

            // Act
            var result = await userController.PutAnnonce(id, annonce);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod()]
        public async Task Put_WithValidId_ReturnsNoContent()
        {
            var mockRepository = new Mock<IRepositoryAnnonce<Annonce>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new AnnoncesController(mockRepository.Object);

            int id = 1; //BonID

            // Act
            var result = await userController.PutAnnonce(id, annonce);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public void DeleteAnnonceTest()
        {
            var mockRepository = new Mock<IRepositoryAnnonce<Annonce>>();
            mockRepository.Setup(x => x.GetById(1)).Returns(testListe[0]);
            var userController = new AnnoncesController(mockRepository.Object);

            //Act
            var resultDest = userController.DeleteAnnonce(1);

            //Assert
            Assert.IsInstanceOfType(resultDest.Result, typeof(ActionResult<Annonce>), "Pas un ActionResult");
            Assert.IsNull(resultDest.Result, "Annonce pas null");
        }
    }
}