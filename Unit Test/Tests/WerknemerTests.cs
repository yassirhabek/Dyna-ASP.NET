using Logic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unit_Test.Stubs;

namespace Unit_Test.Tests
{
    [TestClass]
    public class WerknemerTests
    {
        [TestMethod]
        public void SuccesfulWerknemerAdded()
        {
            // Arrange
            WerknemerDalStub werknemerDalStub = new WerknemerDalStub();
            Werknemer werknemer = new Werknemer("Yassir", 123123, 0612341234, werknemerDalStub);

            // Act
            int result = werknemer.AddWerknemer();

            // Assert
            Assert.AreEqual(1, result);
            Assert.AreEqual(werknemer.Naam, werknemerDalStub.werknemerDTOs[werknemerDalStub.werknemerDTOs.Count - 1].Naam);
            Assert.AreEqual(werknemer.WerknemerNummer, werknemerDalStub.werknemerDTOs[werknemerDalStub.werknemerDTOs.Count - 1].WerknemerNummer);
            Assert.AreEqual(werknemer.TelefoonNummer, werknemerDalStub.werknemerDTOs[werknemerDalStub.werknemerDTOs.Count - 1].TelefoonNummer);
        }

        [TestMethod]
        public void SuccesfulWerknemerUpdated()
        {
            // Arrange

            string naam = "Yassir";
            int werknemerNummer = 123123;
            int telefoonNummer = 0612341234;
            int oldWerknemerID = 4;
            WerknemerDalStub werknemerDalStub = new WerknemerDalStub();

            // Act
            Werknemer werknemer = new Werknemer(naam, werknemerNummer, telefoonNummer, werknemerDalStub);
            int result = werknemer.UpdateWerknemer(oldWerknemerID);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}