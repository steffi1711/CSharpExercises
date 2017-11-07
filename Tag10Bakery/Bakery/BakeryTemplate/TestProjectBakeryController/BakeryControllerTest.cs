using Bakery.Controller;
using Bakery.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Data.Entity;
using Bakery.Model;
using System.Collections.Generic;

namespace TestProjectBakeryController
{
    /// <summary>
    ///This is a test class for BakeryControllerTest and is intended
    ///to contain all BakeryControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BakeryControllerTest
    {


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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            BakeryController.ReadDataFromCsvFilesInDb();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ReadDataFromCsvFilesInDb
        ///</summary>
        [TestMethod()]
        public void T01_ReadDataInDBProductsTest()
        {
            using (BakeryContext context = new BakeryContext())
            {
                Assert.AreEqual(12, context.Products.Count(), " Anzahl der eingelesenen Produkte stimmt nicht");
            }
        }

        /// <summary>
        ///A test for ReadDataFromCsvFilesInDb
        ///</summary>
        [TestMethod()]
        public void T02_ReadDataInDBFullTest()
        {
            using (BakeryContext context = new BakeryContext())
            {
                Assert.AreEqual(12, context.Products.Count(), " Anzahl der eingelesenen Produkte stimmt nicht");
                Assert.AreEqual(8, context.Orders.Count(), " Anzahl der eingelesenen Bestellungen stimmt nicht");
                Assert.AreEqual(4, context.Customers.Count(), " Anzahl der eingelesenen Kunden stimmt nicht");
                Assert.AreEqual(14, context.OrderItems.Count(), " Anzahl der eingelesenen Bestellpositionen stimmt nicht");
            }
        }

        /// <summary>
        ///A test for GetLastNameByOrderNr
        ///</summary>
        [TestMethod()]
        public void T03_GetLastNameByOrderNrTest()
        {
            string orderNr = "2012/4"; 
            string expected = "Sammer"; 
            string actual;
            actual = BakeryController.GetLastNameByOrderNr(orderNr);
            Assert.AreEqual(expected, actual,"Falschen Nachnamen zu Bestellung geliefert!");
        }

        /// <summary>
        ///A test for GetMostExpensiveProductNr
        ///</summary>
        [TestMethod()]
        public void T05_GetMostExpensiveProductNrTest()
        {
            string expected = "P132";
            string actual;
            actual = BakeryController.GetMostExpensiveProductNr();
            Assert.AreEqual(expected, actual, "Falsche Produktnr. geliefert!");
        }

        /// <summary>
        ///A test for GetBestCustomerByProduct
        ///</summary>
        [TestMethod()]
        public void T09_GetBestCustomerByProductTest()
        {
            string productNr = "P122"; 
            string expected = "K107"; 
            string actual;
            actual = BakeryController.GetBestCustomerByProduct(productNr);
            Assert.AreEqual(expected, actual);

            productNr = "P322";
            expected = "K106";
            actual = BakeryController.GetBestCustomerByProduct(productNr);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetSalesPerOrder
        ///</summary>
        [TestMethod()]
        public void T07_GetSalesPerOrderTest()
        {
            string orderNr = "2012/1";
            double expected = 15.4F; 
            double actual;
            actual = BakeryController.GetSalesPerOrder(orderNr);
            Assert.AreEqual(expected, actual,0.01, "Falscher Umsatz zur Bestellung 2012/1");
        }

        /// <summary>
        ///A test for GetOrderCountByCustomer
        ///</summary>
        [TestMethod()]
        public void T04_GetOrderCountByCustomerTest()
        {
            string customerNr = "K106"; 
            int expected = 2; 
            int actual;
            actual = BakeryController.GetOrderCountByCustomer(customerNr);
            Assert.AreEqual(expected, actual, "Falsche Anzahl von Bestellungen");
        }

        /// <summary>
        ///A test for GetInvoices
        ///</summary>
        [TestMethod()]
        public void T08_GetInvoicesTest()
        {
            List<SalesStatistic> actual;
            actual = BakeryController.GetInvoices();
            Assert.AreEqual(4, actual.Count(),"Falsche Anzahl von Listeneinträgen!");
            Assert.AreEqual("Sammer", actual[0].LastName, "Falscher Kunde an erster Listenposition (niedrigster Umsatz)");
            Assert.AreEqual(20.1, actual[0].TotalPrice,0.01, "Falscher Umsatz an erster Listenposition (niedrigster Umsatz)");
            Assert.AreEqual("Müller", actual[3].LastName, "Falscher Kunde an letzter Listenposition (höchster Umsatz)");
            Assert.AreEqual(86.59, actual[3].TotalPrice, 0.01, "Falscher Umsatz an letzter Listenposition (höchster Umsatz)");
        }

        /// <summary>
        ///A test for GetOrderCntByProduct
        ///</summary>
        [TestMethod()]
        public void T06_GetOrderCntByProductTest()
        {
            string productNr = "P122"; 
            int expected = 32; 
            int actual;
            actual = BakeryController.GetOrderCntByProduct(productNr);
            Assert.AreEqual(expected, actual, "Falsche Bestellmenge von Produkt P122");
        }

        /// <summary>
        ///A test for GetOrderCntByProduct
        ///</summary>
        [TestMethod()]
        public void T10_NavigationPropertyTest()
        {
            using (BakeryContext context = new BakeryContext())
            {
                Assert.AreEqual(3, context.Orders.Include("OrderItems").Where(x => x.OrderNr == "2012/1").FirstOrDefault().OrderItems.Count(), "Navigation Property OrderItems in Klasse Order nicht korrekt!");
            }
        }
    }
}
