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
            int count = werknemerContainer.GetUserWerknemers(user.UserID).Count;

            // Assert 
            Assert.AreEqual(2, count);
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

        [TestMethod]
        public void LinkWerknemerToUser()
        {
            //Arrange 
            int werknemerId = 6;
            int userId = 3;
            WerknemerDalStub werknemerDalStub = new WerknemerDalStub();
            Werknemer werknemer = new Werknemer(werknemerDalStub)
            {
                WerknemerID = werknemerId
            };

            //Act
            werknemer.LinkWerknemerToUser(userId);

            //Assert
            Assert.AreEqual(userId, werknemerDalStub.werknemerDTOs[werknemerDalStub.werknemerDTOs.Count - 1].UserID);
            Assert.AreEqual("Hugo", werknemerDalStub.werknemerDTOs[werknemerDalStub.werknemerDTOs.Count - 1].Naam);
            Assert.AreEqual(344874, werknemerDalStub.werknemerDTOs[werknemerDalStub.werknemerDTOs.Count - 1].WerknemerNummer);
            Assert.AreEqual(0622000331, werknemerDalStub.werknemerDTOs[werknemerDalStub.werknemerDTOs.Count - 1].TelefoonNummer);
            Assert.AreEqual(werknemerId, werknemerDalStub.werknemerDTOs[werknemerDalStub.werknemerDTOs.Count - 1].WerknemerID);

        }

        [TestMethod]
        public void ConstructorWerknemer()
        {
            //Arrange
            string name = "Bob";
            int werknemerNummer = 234585;
            int telefoonNummer = 0682849291;
            int werknemerId = 34;

            //Act
            Werknemer werknemer = new Werknemer(werknemerId, name, werknemerNummer, telefoonNummer, new WerknemerDalStub());

            //Assert
            Assert.AreEqual(werknemerId, werknemer.WerknemerID);
            Assert.AreEqual(name, werknemer.Naam);
            Assert.AreEqual(werknemerNummer, werknemer.WerknemerNummer);
            Assert.AreEqual(telefoonNummer, werknemer.TelefoonNummer);
        }

        [TestMethod]
        public void ConstructorCreateWerknemer()
        {
            //Arrange
            string name = "Gerard";
            int werknemerNummer = 573839;
            int telefoonNummer = 0687238294;

            //Act
            Werknemer werknemer = new Werknemer(name, werknemerNummer, telefoonNummer, new WerknemerDalStub());

            //Assert
            Assert.AreEqual(name, werknemer.Naam);
            Assert.AreEqual(werknemerNummer, werknemer.WerknemerNummer);
            Assert.AreEqual(telefoonNummer, werknemer.TelefoonNummer);
        }

        [TestMethod]
        public void ConstructorWerknemerDTO()
        {
            //Arrange
            string name = "Will";
            int werknemerNummer = 7542221;
            int telefoonNummer = 067884921;
            int werknemerId = 89;
            WerknemerDTO werknemerDTO = new WerknemerDTO()
            {
                WerknemerID = werknemerId,
                Naam = name,
                WerknemerNummer = werknemerNummer,
                TelefoonNummer = telefoonNummer
            };

            //Act
            Werknemer werknemer = new Werknemer(werknemerDTO, new WerknemerDalStub());

            //Assert
            Assert.AreEqual(werknemerId, werknemer.WerknemerID);
            Assert.AreEqual(name, werknemer.Naam);
            Assert.AreEqual(werknemerNummer, werknemer.WerknemerNummer);
            Assert.AreEqual(telefoonNummer, werknemer.TelefoonNummer);
        }
    }
}