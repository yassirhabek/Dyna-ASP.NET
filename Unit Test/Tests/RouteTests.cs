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
            Werknemer chauffeur = new Werknemer(1, "Henk", 421223, 0612848910, new WerknemerDalStub());
            Werknemer bijrijder = new Werknemer(2, "Lucas", 282741, 068127282, new WerknemerDalStub());
            TimeSpan startTijd = new TimeSpan(8, 15, 0);
            TimeSpan eindTijd = new TimeSpan(17, 33, 0);
            string bijzonderheden 
            // Act

            // Assert
        }

    }
}
