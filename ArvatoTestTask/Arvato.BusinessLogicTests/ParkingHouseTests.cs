using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arvato.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arvato.Common;
using Arvato.DataAccess;
using Arvato.BusinessLogicTests;


namespace Arvato.BusinessLogicTests
{
    [TestClass()]
    public class ParkingHouseTests
    {
        static AbstractParkingPrices prices = new ParkingPrices();
        static IParkingHouse parking = new DefaultParingHouse("", "", prices);

        [TestMethod]
        public void CarEntersTest()
        {
            parking.CarEnters("111");
            var visit = parking.CarLeave("111");

            Assert.IsNotNull(visit);
            Assert.IsNotNull(visit.Customer);
            Assert.IsTrue(visit.ToPay != 0);
            Assert.IsTrue(visit.Log.Count != 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        [Ignore]
        public void EntereTwiceTest()
        {
            parking.CarEnters("112");
            parking.CarEnters("112");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        [Ignore]
        public void CarTryToLeave()
        {
            parking.CarLeave("113");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        [Ignore]
        public void TestAddPremiumUser()
        {
            parking.AddPremiumCustomer("114", "A", "B");
            parking.AddPremiumCustomer("114", "A", "B");
        }

        [TestMethod]
        public void GetCusomersVisist()
        {
            var allVisits = parking.GetCustomerVisits("111");

            Assert.IsNotNull(allVisits);
            Assert.IsTrue(allVisits.Count == 1);
        }

        [TestMethod]
        public void GetInvoicesTest()
        {
            var currentMonth = (Moths)DateTime.Now.Month;
            var invoices = parking.GetAllCustomersInvoicesForMonth(currentMonth);

            Assert.IsNotNull(invoices);
            Assert.IsTrue(invoices.Count == 3);
            var totalIncomeShouldBe = 0+ 20 + 1.5;
            Assert.IsTrue(invoices.Sum(i => i.Total) == totalIncomeShouldBe);
        }
    }
}
