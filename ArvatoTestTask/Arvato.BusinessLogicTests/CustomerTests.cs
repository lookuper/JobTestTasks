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
    public class CustomerTests
    {
        [TestMethod]
        public void GetInvoiceTest()
        {
            var customer = TestData.PremiumCustomer;
            var month = (Moths)TestData.DefaultDateTime.Month;
            var invoice = customer.GetInvoice(month);

            Assert.IsTrue(!String.IsNullOrEmpty(invoice.Customer));
            Assert.IsTrue(invoice.Month == month);
            Assert.IsTrue(invoice.Total == 38.25);
            Assert.IsTrue(invoice.Visits.Count != 0);                
        }

        [TestMethod]
        public void GetInvoicePremiumTest()
        {
            var customer = TestData.PremiumCustomer;
            var maxVisit = new Visit(customer)
            {
                EnterTime = TestData.DefaultDateTime,
                LeaveTime = TestData.DefaultDateTime.AddDays(10),
                ToPay = 280,
            };

            customer.CustomerVisits.Add(maxVisit);
            var month = (Moths)TestData.DefaultDateTime.Month;
            var invoice = customer.GetInvoice(month);

            Assert.IsTrue(!String.IsNullOrEmpty(invoice.Customer));
            Assert.IsTrue(invoice.Month == month);
            Assert.IsTrue(invoice.Total == 300);
            Assert.IsTrue(invoice.Visits.Count != 0);
        }
    }
}
