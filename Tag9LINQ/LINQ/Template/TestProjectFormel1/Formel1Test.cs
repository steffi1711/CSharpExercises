using F1Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace TestProjectFormel1
{
    
    
    /// <summary>
    ///This is a test class for Formel1Test and is intended
    ///to contain all Formel1Test Unit Tests
    ///</summary>
    [TestClass()]
    public class Formel1Test
    {
        Controller controller;

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //    string path = Environment.CurrentDirectory; //AppDomain.CurrentDomain.BaseDirectory;
        //    DirectoryInfo dir = new DirectoryInfo(path);
        //    FileInfo[] files = dir.GetFiles(csvFileName);
        //    while (dir != null && files.Length == 0)
        //    // gibt es Verzeichnis noch und file noch nicht eingelesen
        //    {
        //        dir = dir.Parent;
        //        if (dir != null)
        //        {
        //            files = dir.GetFiles(csvFileName);
        //        }
        //    }
        //    if (files.Length > 0)  // Datei existiert
        //    {
        //        fullFileName = files[0].FullName;
        //    }
        //    else
        //    {
        //        throw new FileNotFoundException("Datei " + csvFileName + " existiert nicht");
        //    }
        //}


        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        // Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            controller = new Controller();
            controller.ReadDataFromCsvFiles();
        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod()]
        public void T01_ReadRacesDriversTeamsAndResults()
        {
            Assert.AreEqual(20, controller.Races.Count(), " Anzahl der eingelesenen Rennen stimmt nicht");
            Assert.AreEqual(25, controller.Drivers.Count(), " Anzahl der eingelesenen Fahrer stimmt nicht");
            Assert.AreEqual(12, controller.Teams.Count(), " Anzahl der eingelesenen Teams stimmt nicht");
            Assert.AreEqual(279, controller.Results.Count(), " Anzahl der eingelesenen Ergebnisse stimmt nicht");
        }

        /// <summary>
        ///A test for GetResultsFromCsv
        ///</summary>
        [TestMethod()]
        public void T02_GetResultsFromCsvTest()
        {
            var results = controller.Results.ToArray();
            Assert.AreEqual(279, results.Length, "Anzahl Results stimmt nicht!");
            Assert.AreEqual("Button", results[0].Driver.LastName);  // Erster Saisonsieger
        }

        /// <summary>
        /// Aktuellen WM-Stand ermitteln
        ///</summary>
        [TestMethod()]
        public void T03_GetDriverWMTableTest()
        {
            var actual = controller.GetDriverWMTable().ToArray();
            Assert.AreEqual(25, actual.Length, "Anzahl der Fahrer stimmt nicht");
            Assert.AreEqual(194, actual[0].Points, "Punkte des Führenden stimmen nicht");
            Assert.AreEqual(51, actual[9].Points, "Punkte des 10. stimmen nicht");
            Assert.AreEqual("Alonso", actual[0].Name, "Name des WM-Führenden stimmt nicht");
        }

        /// <summary>
        /// Teamtabelle aus Fahrerergebnissen ermitteln
        /// </summary>
        [TestMethod()]
        public void T04_GetTeamWMTableTest()
        {
            var actual = controller.GetTeamWMTable().ToArray();
            Assert.AreEqual(12, actual.Length, "Anzahl der Teams stimmt nicht");
            Assert.AreEqual(297, actual[0].Points, "Punkte des führenden Teams stimmen nicht");
            Assert.AreEqual("Red Bull Racing", actual[0].Name, "Name des führenden Teams stimmt nicht");
        }

    }
}
