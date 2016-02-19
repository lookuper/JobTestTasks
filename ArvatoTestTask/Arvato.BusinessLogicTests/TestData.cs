using Arvato.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.BusinessLogicTests
{
    public class TestData
    {
        public static DateTime DefaultDateTime = new DateTime(2016, 2, 18);

        public static Customer RegularCustomer = new RegularCustomer("r1")
        {
            FirstName = "C",
            LastName = "R",
            CustomerVisits = new List<Visit>()
            {
                new Visit() { EnterTime = DefaultDateTime.AddHours(8).AddMinutes(12), LeaveTime = DefaultDateTime.AddHours(10).AddMinutes(45) },
                new Visit() { EnterTime = DefaultDateTime.AddHours(19).AddMinutes(40), LeaveTime = DefaultDateTime.AddHours(20).AddMinutes(35) },
            }
        };

        public static Customer PremiumCustomer = new PremiumCustomer("p1")
        {
            FirstName = "C",
            LastName = "P",
            CustomerVisits = new List<Visit>()
            {
                new Visit() { EnterTime = DefaultDateTime.AddHours(8).AddMinutes(12), LeaveTime = DefaultDateTime.AddHours(10).AddMinutes(45) },
                new Visit() { EnterTime = DefaultDateTime.AddHours(7).AddMinutes(2), LeaveTime = DefaultDateTime.AddHours(11).AddMinutes(56) },
                new Visit() { EnterTime = DefaultDateTime.AddHours(22).AddMinutes(10), LeaveTime = DefaultDateTime.AddHours(22).AddMinutes(35) },
                new Visit() { EnterTime = DefaultDateTime.AddHours(19).AddMinutes(40), LeaveTime = DefaultDateTime.AddHours(20).AddMinutes(35) },
            }
        };
    }
}
