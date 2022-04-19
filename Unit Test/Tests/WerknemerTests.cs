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
            string naam = "Yassir";
            int werknemerNummer = 123123;
            int telefoonNummer = 0612341234;
            WerknemerDalStub werknemerDalStub = new WerknemerDalStub();

            // Act
            Werknemer werknemer = new Werknemer(naam, werknemerNummer, telefoonNummer, werknemerDalStub);
            int result = werknemer.AddWerknemer();

            // Assert
            Assert.AreEqual(1, result);
            Assert.AreEqual(naam, werknemerDalStub.werknemerDTOs[werknemerDalStub.werknemerDTOs.Count - 1].Naam);
            Assert.AreEqual(werknemerNummer, werknemerDalStub.werknemerDTOs[werknemerDalStub.werknemerDTOs.Count - 1].NummerPasje);
            Assert.AreEqual(telefoonNummer, werknemerDalStub.werknemerDTOs[werknemerDalStub.werknemerDTOs.Count - 1].TelefoonNummer);
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