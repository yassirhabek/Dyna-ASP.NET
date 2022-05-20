using Interfaces.DTO;
using Logic.Containers;
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
            User user = new User()
            {
                UserID = 1
            };
            WerknemerDalStub werknemerDalStub = new WerknemerDalStub();
            Werknemer werknemer = new Werknemer("Yassir", 123123, 0612341234, werknemerDalStub);

            // Act
            int result = werknemer.AddWerknemer(user.UserID);

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
            string naam = "Harry";
            int werknemerNummer = 123123;
            int telefoonNummer = 0612341234;
            int oldWerknemerID = 4;
            WerknemerDalStub werknemerDalStub = new WerknemerDalStub();

            // Act
            Werknemer werknemer = new Werknemer(naam, werknemerNummer, telefoonNummer, werknemerDalStub);
            int result = werknemer.UpdateWerknemer(oldWerknemerID);

            // Assert
            Assert.AreEqual(1, result);
            Assert.AreEqual(werknemer.Naam, werknemerDalStub.werknemerDTOs[werknemerDalStub.werknemerDTOs.Count - 1].Naam);
            Assert.AreEqual(werknemer.WerknemerNummer, werknemerDalStub.werknemerDTOs[werknemerDalStub.werknemerDTOs.Count - 1].WerknemerNummer);
            Assert.AreEqual(werknemer.TelefoonNummer, werknemerDalStub.werknemerDTOs[werknemerDalStub.werknemerDTOs.Count - 1].TelefoonNummer);
        }

        [TestMethod]
        public void SuccesfulWerknemerDeleted()
        {
            // Arrange
            string naam = "Harry";
            int werknemerNummer = 123123;
            int telefoonNummer = 0612341234;
            int oldWerknemerID = 4;
            WerknemerDalStub werknemerDalStub = new WerknemerDalStub();

            // Act
            Werknemer werknemer = new Werknemer(naam, werknemerNummer, telefoonNummer, werknemerDalStub);
            int result = werknemer.DeleteWerknemer();

            // Assert
            Assert.AreEqual(1, result);
            Assert.IsFalse(werknemerDalStub.werknemerDTOs.Exists(w => w.WerknemerID == werknemer.WerknemerID));
        }

        [TestMethod]
        public void SuccesfulGetWerknemers()
        {
            // Arrange
            User user = new User()
            {
                UserID = 1
            };
            WerknemerContainer werknemerContainer = new WerknemerContainer(new WerknemerDalStub());

            // Act
            int count = werknemerContainer.GetWerknemers(user.UserID).Count;

            // Assert 
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void SuccesfulGetSingleWerknemers()
        {
            // Arrange
            int WerknemerID = 4;
            string Naam = "Daniel";
            int WerknemerNummer = 123321;
            int TelefoonNummer = 0619244212;
            WerknemerContainer werknemerContainer = new WerknemerContainer(new WerknemerDalStub());

            // Act
            Werknemer newWerknemer = new Werknemer(WerknemerID, Naam, WerknemerNummer, TelefoonNummer, new WerknemerDalStub());
            Werknemer searchedWerknemer = werknemerContainer.GetSingleWerknemer(4);

            // Assert
            Assert.AreEqual(newWerknemer.WerknemerID, searchedWerknemer.WerknemerID);
            Assert.AreEqual(newWerknemer.Naam, searchedWerknemer.Naam);
            Assert.AreEqual(newWerknemer.WerknemerNummer, searchedWerknemer.WerknemerNummer);
            Assert.AreEqual(newWerknemer.TelefoonNummer, searchedWerknemer.TelefoonNummer);
        }
    }
}