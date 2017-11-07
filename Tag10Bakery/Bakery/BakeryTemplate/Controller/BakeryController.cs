using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;
using Bakery.Model;
using Bakery.DataAccess;
using System.Data.Entity;

namespace Bakery.Controller
{
    public static class BakeryController
    {
        /// <summary>
        /// Liest die Daten aus den csv-Files und trägt diese in die Datenbank ein
        /// </summary>
        public static void ReadDataFromCsvFilesInDb()
        {
            string[][] productsCsv = "Products.csv".ReadStringMatrixFromCsv(true);
            IList<Product> products = productsCsv.Select(line =>
                new Product()
                {
                    ProductNr = line[0],
                    Name = line[1],
                    Price = Convert.ToDouble(line[2])
                }).ToList();

            string[][] ordersCsv = "OrderItems.csv".ReadStringMatrixFromCsv(true);
            //IList<Customer> customers = ordersCsv.GroupBy(line => line[2]).Select(custGrp =>
            //    new Customer
            //    {
            //        CustomerNr=custGrp.Key,
            //        FirstName=custGrp.First()[4],
            //        LastName=custGrp.First()[3]
            //    }).ToList();

            //Alternativ
            IList<Customer> customers = ordersCsv.GroupBy(line =>
                 new
                 {
                     CustNr = line[2],
                     FirstName = line[4],
                     LastName = line[3]
                 }).Select(custGrp =>
                new Customer
                {
                    CustomerNr = custGrp.Key.CustNr,
                    FirstName = custGrp.Key.FirstName,
                    LastName = custGrp.Key.LastName
                }).ToList();

            IList<Order> orders = ordersCsv.GroupBy(line => line[0]).Select(orderGrp =>
                  new Order
                  {
                      OrderNr = orderGrp.Key,
                      Date = Convert.ToDateTime(orderGrp.First()[1]),
                      Customer = customers.Where(cust => cust.CustomerNr == orderGrp.First()[2]).SingleOrDefault(),
                  }).ToList();

            IList<OrderItems> items = ordersCsv.Select(item =>
                new OrderItems
                {
                    Amount = Convert.ToInt32(item[6]),
                    Order = orders.Where(order => order.OrderNr == item[0]).SingleOrDefault(),
                    Product = products.Where(prod => prod.ProductNr == item[5]).SingleOrDefault()
                }).ToList();

            using (BakeryContext context = new BakeryContext())
            {
                context.Products.AddRange(products);

                foreach (var customer in customers)
                {
                    context.Customers.Add(customer);
                }

                foreach (var order in orders)
                {
                    context.Orders.Add(order);
                }

                foreach (var item in items)
                {
                    context.OrderItems.Add(item);
                }

                context.SaveChanges();
            }
        }
        /// <summary>
        /// Level: 1
        /// Liefert den Familiennamen des Kunden zu einer bestimmten Bestellnr. zurück
        /// </summary>
        /// <param name="orderNr"></param>
        /// <returns></returns>
        public static string GetLastNameByOrderNr(string orderNr)
        {
            using (BakeryContext context = new BakeryContext())
            {
                var cont = context.Orders.Include(ord => ord.Customer).Where(w => w.OrderNr == orderNr).FirstOrDefault().Customer.LastName;
                return cont;
            }
           
        }

        /// <summary>
        /// Level: 1
        /// Liefert die Anzahl an Bestellung (NICHT BESTELLPOSITIONEN) eines Kunden
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static int GetOrderCountByCustomer(string customerNr)
        {
            using (BakeryContext context = new BakeryContext())
            {
                return context.Orders.Where(order => order.Customer.CustomerNr == customerNr).Count();
            }
        }

        /// <summary>
        /// Level: 1
        /// Liefert die Produktnr. des Produkts mit dem höchsten Preis (wenn zwei Produkte einen gleich hohen Preis haben, soll das Produkt mit der 
        /// höheren Nummer zurückgeliefert werden.
        /// </summary>
        /// <returns></returns>
        public static string GetMostExpensiveProductNr()
        {
            using (BakeryContext context = new BakeryContext())
            {
                return context.Products.OrderByDescending(prod => prod.Price).ThenByDescending(prod => prod.ProductNr).FirstOrDefault().ProductNr;
            }
        }

        /// <summary>
        /// Level: 1
        /// Liefert die gesamte bestellte Menge zu einem bestimmten Produkt
        /// </summary>
        /// <param name="productNr"></param>
        /// <returns></returns>
        public static int GetOrderCntByProduct(string productNr)
        {
            using (BakeryContext context = new BakeryContext())
            {
                return context.OrderItems.Where(items => items.Product.ProductNr == productNr).Sum(item => item.Amount);
            }
        }

        /// <summary>
        /// Level: 2
        /// Liefert den Gesamtumsatz einer Bestellung
        /// </summary>
        /// <param name="orderNr"></param>
        /// <returns></returns>
        public static double GetSalesPerOrder(string orderNr)
        {
            using (BakeryContext context = new BakeryContext())
            {
                return context.OrderItems.Where(items => items.Order.OrderNr == orderNr).Sum(item => (item.Amount * item.Product.Price));
            }
        }      

        /// <summary>
        /// Level: 2
        /// Es ist eine Liste mit dem Umsatz je Kunden zu returnieren (Vorname, Nachname, Rechnungsbetrag).
        /// Die Klasse der zurückzuliefernden Listenelemente ist bereits vorgegeben (SalesStatistic)
        /// Liste soll aufsteigend nach Rechnungsbetrag sortiert werden.
        /// </summary>
        /// <returns></returns>
        public static List<SalesStatistic> GetInvoices()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Level: 3 
        /// Liefert denjenigen Kunden der von dem übergebenen Produkt in Summe am meisten bestellt hat.
        /// </summary>
        /// <param name="productNr"></param>
        /// <returns></returns>
        public static string GetBestCustomerByProduct(string productNr)
        {
            throw new NotImplementedException();
        }
    }
}
