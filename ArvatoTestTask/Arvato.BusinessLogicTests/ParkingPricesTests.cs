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

namespace Arvato.BusinessLogic.Tests
{
    [TestClass()]
    public class ParkingPricesTests
    {
        private AbstractParkingPrices testPrice = new ParkingPrices();
        private SimpleDataModel dataModel = new SimpleDataModel();            

        [TestMethod()]
        public void CalculateVisitPriceLessPaidTime()
        {
            var customer = SimpleDataModel.RegularCustomer;
            customer.CustomerPrices = testPrice;
            var visit = customer.CustomerVisits.First();

            double expected = 1;
            var actual = testPrice.CalculateVisitPrice(customer, visit);
            Assert.IsTrue(expected == actual);
        }

        [TestMethod()]
        public void CalculateVisitPriceNotBusinnesHours()
        {
            var customer = SimpleDataModel.RegularCustomer;
            customer.CustomerPrices = testPrice;
            var visit = customer.CustomerVisits[1];

            double expected = 7;
            var actual = testPrice.CalculateVisitPrice(customer, visit);
            Assert.IsTrue(expected == actual);
        }

        [TestMethod()]
        public void CalculateVisitPriceBusinnesHours()
        {
            var customer = SimpleDataModel.RegularCustomer;
            customer.CustomerPrices = testPrice;
            var visit = customer.CustomerVisits[2];

            double expected = 10.5;
            var actual = testPrice.CalculateVisitPrice(customer, visit);
            Assert.IsTrue(expected == actual);
        }

        [TestMethod()]
        public void CalculateVisitPriceBusinnesHours2()
        {
            var customer = SimpleDataModel.RegularCustomer;
            customer.CustomerPrices = testPrice;
            var visit = customer.CustomerVisits.Last();

            double expected = 131.5;
            var actual = testPrice.CalculateVisitPrice(customer, visit);
            Assert.IsTrue(expected == actual);
        }

        [TestMethod]
        public void CalculateExampleForRegularCustomer()
        {
            var customer = TestData.RegularCustomer;
            customer.CustomerPrices = testPrice;
            var visit = customer.CustomerVisits[0];
            Assert.IsTrue(testPrice.CalculateVisitPrice(customer, visit) == 9);

            visit = customer.CustomerVisits[1];
            Assert.IsTrue(testPrice.CalculateVisitPrice(customer, visit) == 2);
        }

        [TestMethod]
        public void CalculateExamplesForPremiumCustomer()
        {
            var customer = TestData.PremiumCustomer;
            customer.CustomerPrices = testPrice;
            var visit = customer.CustomerVisits[0];
            Assert.IsTrue(testPrice.CalculateVisitPrice(customer, visit) == 6);

            visit = customer.CustomerVisits[1];
            Assert.IsTrue(testPrice.CalculateVisitPrice(customer, visit) == 10);

            visit = customer.CustomerVisits[2];
            Assert.IsTrue(testPrice.CalculateVisitPrice(customer, visit) == 0.75);

            visit = customer.CustomerVisits[3];
            Assert.IsTrue(testPrice.CalculateVisitPrice(customer, visit) == 1.50);
        }
    }
}