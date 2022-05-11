using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Logic.Models;
using Unit_Test.Stubs;

namespace Unit_Test.Tests
{
    [TestClass]
    public class RouteTests
    {
        [TestMethod]
        public void SuccesfulRouteAdded()
        {
            // Arrange
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
            int result = route.AddRoute();

            // Assert
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
        public void SuccesfulRouteUpdated()
        {

        }
    }
}
