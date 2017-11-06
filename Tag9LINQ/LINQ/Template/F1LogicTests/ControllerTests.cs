using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace F1Logic.Tests
{
    [TestClass]
    public class ControllerTests
    {
        private Controller _controller;

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void MyTestInitialize()
        {
            _controller = new Controller();
            _controller.ReadDataFromCsvFiles();
        }


        [TestMethod]
        public void T01_ReadRacesDriversTeamsAndResults()
        {
            Assert.AreEqual(19, _controller.Races.Count(), " Anzahl der eingelesenen Rennen stimmt nicht");
            Assert.AreEqual(22, _controller.Drivers.Count(), " Anzahl der eingelesenen Fahrer stimmt nicht");
            Assert.AreEqual(11, _controller.Teams.Count(), " Anzahl der eingelesenen Teams stimmt nicht");
            Assert.AreEqual(236, _controller.Results.Count(), " Anzahl der eingelesenen Ergebnisse stimmt nicht");
        }

        /// <summary>
        ///     A test for GetResultsFromCsv
        /// </summary>
        [TestMethod]
        public void T02_GetResultsFromCsvTest()
        {
            Result[] results = _controller.Results.ToArray();
            Assert.AreEqual(236, results.Length, "Anzahl Results stimmt nicht!");
            Assert.AreEqual("Rosberg", results[0].Driver.LastName); // Erster Saisonsieger
        }

        /// <summary>
        ///     Aktuellen WM-Stand ermitteln
        /// </summary>
        [TestMethod]
        public void T03_GetDriverWMTableTest()
        {
            ResultLine[] actual = _controller.GetDriverWmTable().ToArray();
            Assert.AreEqual(22, actual.Length, "Anzahl der Fahrer stimmt nicht");
            Assert.AreEqual(241, actual[0].Points, "Punkte des Führenden stimmen nicht");
            Assert.AreEqual(45, actual[9].Points, "Punkte des 10. stimmen nicht");
            Assert.AreEqual("Hamilton", actual[0].Name, "Name des WM-Führenden stimmt nicht");
        }

        /// <summary>
        ///     Teamtabelle aus Fahrerergebnissen ermitteln
        /// </summary>
        [TestMethod]
        public void T04_GetTeamWMTableTest()
        {
            ResultLine[] actual = _controller.GetTeamWmTable().ToArray();
            Assert.AreEqual(11, actual.Length, "Anzahl der Teams stimmt nicht");
            Assert.AreEqual(479, actual[0].Points, "Punkte des führenden Teams stimmen nicht");
            Assert.AreEqual("Mercedes", actual[0].Name, "Name des führenden Teams stimmt nicht");
        }
    }
}