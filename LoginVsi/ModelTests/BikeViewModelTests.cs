using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Tests
{
    [TestClass()]
    public class BikeViewModelTests
    {
        [TestMethod()]
        public void BackWheelTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StandardPrice()
        {
            var testBike = new BikeViewModel();
            Assert.IsTrue(12 == testBike.TotalPrice);
        }

        [TestMethod()]
        public void ZeroPrice()
        {
            var testBike = new BikeViewModel();
            testBike.AvaliableParts.ToList().ForEach(v => v.Price = 0);
            Assert.IsTrue(0 == testBike.TotalPrice);
        }

        //[TestMethod()]
        //[ExpectedException(typeof(NullReferenceException))]
        //public void ZeroPrice()
        //{
        //    var testBike = new Bike();
        //    testBike.AvaliableParts = null;
        //    Assert.IsTrue(0 == testBike.TotalPrice);
        //}
    }
}