using BankAccount.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BankAccount.TestProject
{
    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void TestCheckIban()
        {
            Assert.IsTrue(Account.CheckIBAN("LV80BANK0000435195001"), "LV80BANK0000435195001 sollte gültig sein!");
            Assert.IsTrue(Account.CheckIBAN("AT611904300234573201"), "AT611904300234573201 sollte gültig sein!");
            Assert.IsTrue(Account.CheckIBAN("BG80BNBG96611020345678"), "BG80BNBG96611020345678 sollte gültig sein!");
            Assert.IsTrue(Account.CheckIBAN("DK5000400440116243"), "DK5000400440116243 sollte gültig sein!");
            Assert.IsTrue(Account.CheckIBAN("MT84MALT011000012345MTLCAST001S"), "MT84MALT011000012345MTLCAST001S sollte gültig sein!");
            Assert.IsFalse(Account.CheckIBAN("LV80BANK0000635195001"), "LV80BANK0000635195001 sollte ungültig sein!");
            Assert.IsFalse(Account.CheckIBAN("MT84MALT011000013345MTLCAST001S"), "MT84MALT011000013345MTLCAST001S sollte ungültig sein!");
            Assert.IsFalse(Account.CheckIBAN("AT5827772636367292929"), "AT5827772636367292929 sollte ungültig sein!");
            Assert.IsFalse(Account.CheckIBAN("LV80BANZ0000635195001"), "LV80BANZ0000635195001 sollte ungültig sein!");
        }


        //[TestMethod]
        //public void GetOverdrawnDays_TestSimple()
        //{

        //    List<Transaction> trans = new List<Transaction>();
        //    trans.Add(new Transaction() { Date = new DateTime(2013, 1, 1), Amount = 1000 });

        //    trans.Add(new Transaction() { Date = new DateTime(2013, 1, 5), Amount = -2000 });
        //    trans.Add(new Transaction() { Date = new DateTime(2013, 1, 10), Amount = 3000 });
        //    trans.Add(new Transaction() { Date = new DateTime(2013, 1, 12), Amount = 3000 });

        //    int result = UnitOfWork.GetOverdrawnDays(trans);
        //    Assert.AreEqual(6, result, "Berechnung sollte 6 Tage negativ ergeben!");
        //}

        //[TestMethod]
        //public void GetOverdrawnDays_TestComplex()
        //{

        //    List<Transaction> trans = new List<Transaction>();
        //    trans.Add(new Transaction() { Date = new DateTime(2013, 1, 1), Amount = 1000 });
        //    trans.Add(new Transaction() { Date = new DateTime(2013, 1, 5), Amount = -2000 });
        //    trans.Add(new Transaction() { Date = new DateTime(2013, 1, 7), Amount = -500 });
        //    trans.Add(new Transaction() { Date = new DateTime(2013, 1, 10), Amount = +3500 });
        //    trans.Add(new Transaction() { Date = new DateTime(2013, 1, 12), Amount = +3000 });
        //    trans.Add(new Transaction() { Date = new DateTime(2013, 1, 14), Amount = -5000 });
        //    trans.Add(new Transaction() { Date = new DateTime(2013, 1, 15), Amount = -7000 });
        //    trans.Add(new Transaction() { Date = new DateTime(2013, 1, 15), Amount = +7500 });

        //    int result = UnitOfWork.GetOverdrawnDays(trans);
        //    Assert.AreEqual(7, result, "Berechnung sollte 7 Tage negativ ergeben!");

        //    trans.Add(new Transaction() { Date = new DateTime(2013, 1, 17), Amount = -7000 });
        //    trans.Add(new Transaction() { Date = new DateTime(2013, 1, 18), Amount = +7000 });

        //    result = UnitOfWork.GetOverdrawnDays(trans);
        //    Assert.AreEqual(9, result, "Berechnung sollte 9 Tage negativ ergeben!");
        //}
    }
}

