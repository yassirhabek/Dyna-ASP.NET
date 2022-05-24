using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Logic.Models;
using Unit_Test.Stubs;
using Logic.Containers;

namespace Unit_Test.Tests
{
    [TestClass]
    public class RouteTests
    {
        [TestMethod]
        public void SuccesfulRouteAdded()
        {
            // Arrange
            User user = new User()
            {
                UserID = 1
            };
            int routeNummer = 3214;
            DateTime datum = DateTime.Today;
            Werknemer chauffeur = new Werknemer(3, "Martijn", 421223, 0612848910, new WerknemerDalStub());
            Werknemer bijrijder = new Werknemer(4, "Paul", 282741, 068127282, new WerknemerDalStub());
            TimeSpan startTijd = new TimeSpan(8, 15, 0);
            TimeSpan eindTijd = new TimeSpan(17, 33, 0);
            string bijzonderheden = "niks";
            RouteDalStub routeDalStub = new RouteDalStub();

            // Act
            RouteRit route = new RouteRit(routeNummer, datum, chauffeur, bijrijder, startTijd, eindTijd, bijzonderheden, routeDalStub);
            int result = route.AddRoute(user.UserID);

            // Assert
            Assert.AreEqual(1, result);
            Assert.AreEqual(route.RouteNummer, routeDalStub.RouteDTOs[routeDalStub.RouteDTOs.Count - 1].RouteNummer);
            Assert.AreEqual(route.Datum, routeDalStub.RouteDTOs[routeDalStub.RouteDTOs.Count - 1].Datum);
            Assert.AreEqual(route.Chauffeur.WerknemerID, routeDalStub.RouteDTOs[routeDalStub.RouteDTOs.Count - 1].Chauffeur.WerknemerID);
            Assert.AreEqual(route.BijRijder.WerknemerID, routeDalStub.RouteDTOs[routeDalStub.RouteDTOs.Count - 1].BijRijder.WerknemerID);
            Assert.AreEqual(route.StartTijd, routeDalStub.RouteDTOs[routeDalStub.RouteDTOs.Count - 1].StartTijd);
            Assert.AreEqual(route.EindTijd, routeDalStub.RouteDTOs[routeDalStub.RouteDTOs.Count - 1].EindTijd);
            Assert.AreEqual(route.AantalUur, routeDalStub.RouteDTOs[routeDalStub.RouteDTOs.Count - 1].AantalUur);
            Assert.AreEqual(route.Bijzonderheden, routeDalStub.RouteDTOs[routeDalStub.RouteDTOs.Count - 1].Bijzonderheden);
        }

        [TestMethod]
        public void SuccesfulRouteUpdated()
        {
            // Arrange
            User user = new User()
            {
                UserID = 1
            };
            int routeID = 3;
            int routeNummer = 3214;
            DateTime datum = DateTime.Today;
            Werknemer chauffeur = new Werknemer(3, "Martijn", 421223, 0612848910, new WerknemerDalStub());
            Werknemer bijrijder = new Werknemer(4, "Paul", 282741, 068127282, new WerknemerDalStub());
            TimeSpan startTijd = new TimeSpan(8, 15, 0);
            TimeSpan eindTijd = new TimeSpan(17, 33, 0);
            string bijzonderheden = "niks";
            RouteDalStub routeDalStub = new RouteDalStub();

            //Act
            RouteRit route = new RouteRit(routeNummer, datum, chauffeur, bijrijder, startTijd, eindTijd, bijzonderheden, routeDalStub);
            int result = route.UpdateRoute(routeID);

            //Assert
            Assert.AreEqual(1, result);
            Assert.AreEqual(route.RouteNummer, routeDalStub.RouteDTOs[routeDalStub.RouteDTOs.Count - 1].RouteNummer);
            Assert.AreEqual(route.Datum, routeDalStub.RouteDTOs[routeDalStub.RouteDTOs.Count - 1].Datum);
            Assert.AreEqual(route.Chauffeur.WerknemerID, routeDalStub.RouteDTOs[routeDalStub.RouteDTOs.Count - 1].Chauffeur.WerknemerID);
            Assert.AreEqual(route.BijRijder.WerknemerID, routeDalStub.RouteDTOs[routeDalStub.RouteDTOs.Count - 1].BijRijder.WerknemerID);
            Assert.AreEqual(route.StartTijd, routeDalStub.RouteDTOs[routeDalStub.RouteDTOs.Count - 1].StartTijd);
            Assert.AreEqual(route.EindTijd, routeDalStub.RouteDTOs[routeDalStub.RouteDTOs.Count - 1].EindTijd);
            Assert.AreEqual(route.Bijzonderheden, routeDalStub.RouteDTOs[routeDalStub.RouteDTOs.Count - 1].Bijzonderheden);
        }

        [TestMethod]
        public void SuccesfulRouteRemoved()
        {
            //Arrange
            User user = new User()
            {
                UserID = 1
            };
            int routeID = 1;
            int routeNummer = 4214;
            DateTime datum = new DateTime(2022, 6, 27);
            Werknemer chauffeur = new Werknemer(1, "Harry", 12342, 0612842912, new WerknemerDalStub());
            Werknemer bijrijder = new Werknemer(2, "Lucas", 42341, 0682848211, new WerknemerDalStub());
            TimeSpan startTijd = new TimeSpan(8, 15, 0);
            TimeSpan eindTijd = new TimeSpan(17, 15, 0);
            string bijzonderheden = "geen";
            RouteDalStub routeDalStub = new RouteDalStub();

            //Act
            RouteRit route = new RouteRit(routeID, routeNummer, datum, chauffeur, bijrijder, startTijd, eindTijd, bijzonderheden, routeDalStub);
            int result = route.DeleteRoute();

            //Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(routeDalStub.RouteDTOs.Exists(r => r.RouteID == route.RouteID));
        }

        [TestMethod]
        public void SuccesfulGetRoutes()
        {
            //Arrange
            User user = new User()
            {
                UserID = 1
            };
            RouteContainer routeContainer = new RouteContainer(new RouteDalStub());

            //Act
            int count = routeContainer.GetRoutes(new WerknemerDalStub(), new WerknemerDalStub(), user.UserID).Count;

            //Assert
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void SuccesGetRouteByDate()
        {
            //Arrange
            User user = new User()
            {
                UserID = 1
            };
            string rawDate = "27/6/2022";
            RouteContainer routeContainer = new RouteContainer(new RouteDalStub());

            //Act
            int count = routeContainer.GetRoutesFromDate(rawDate, new WerknemerDalStub(), new WerknemerDalStub(), user.UserID).Count;

            //Assert
            Assert.AreEqual(1, count);  
        }

        [TestMethod]
        public void SuccesfulGetRouteByMonth()
        {
            //Arrange
            User user = new User()
            {
                UserID = 1
            };
            string rawDate = "2022-06";
            RouteContainer routeContainer = new RouteContainer(new RouteDalStub());

            //Act
            int count = routeContainer.GetRoutesByMonth(rawDate, new WerknemerDalStub(), new WerknemerDalStub(), user.UserID).Count;

            //Assert
            Assert.AreEqual(2, count);
        }
    }
}
