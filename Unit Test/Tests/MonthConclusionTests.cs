using Logic.Containers;
using Logic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Test.Stubs;

namespace Unit_Test.Tests
{
    [TestClass]
    public class MonthConclusionTests
    {
        [TestMethod]
        public void GetMonthConclusionMultiRoutes()
        {
            //Arrange
            string monthyear = "2022-06";
            MonthConclusionDalStub monthConclusionDalStub = new MonthConclusionDalStub();
            MonthConclusionContainer monthConclusionContainer = new MonthConclusionContainer(monthConclusionDalStub);

            //Act
            MonthConclusion monthConclusion = monthConclusionContainer.CalcMonthConclusion(monthyear, new WerknemerDalStub(), new WerknemerDalStub(), 1, new RouteDalStub());

            //Assert
            Assert.AreEqual(2, monthConclusion.AantalRoutesGereden);
            Assert.AreEqual(new TimeSpan(15, 30, 0), monthConclusion.AantalUren);
        }

        [TestMethod]
        public void GetMonthConclusionSingleRoute()
        {
            //Arrange
            string monthyear = "2022-07";
            MonthConclusionDalStub monthConclusionDalStub = new MonthConclusionDalStub();
            MonthConclusionContainer monthConclusionContainer = new MonthConclusionContainer(monthConclusionDalStub);

            //Act
            MonthConclusion monthConclusion = monthConclusionContainer.CalcMonthConclusion(monthyear, new WerknemerDalStub(), new WerknemerDalStub(), 1, new RouteDalStub());

            //Assert
            Assert.AreEqual(1, monthConclusion.AantalRoutesGereden);
            Assert.AreEqual(new TimeSpan(11, 0, 0), monthConclusion.AantalUren);
        }
    }
}
