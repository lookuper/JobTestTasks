using Arvato.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.DataAccess
{
    public class SimpleDataModel
    {
        public static List<Customer> AllCustomers
        {
            get
            {
                return new List<Customer> { SimpleDataModel.RegularCustomer, SimpleDataModel.PremiumCustomer };
            }
        }

        public static Customer RegularCustomer
        {
            get
            {
                var now = DateTime.Now;
                var visitsReg = new List<Visit>()
                {
                    new Visit("1-1") {EnterTime = now, LeaveTime = now.AddMinutes(29) },
                    new Visit("1-1") {EnterTime = now, LeaveTime = now.AddHours(3).AddMinutes(12) },
                    new Visit("1-1") {EnterTime = now, LeaveTime = now.AddDays(2).AddHours(3).AddMinutes(12) },
                };

                var customer = new RegularCustomer
                {
                    FirstName = "User",
                    LastName = "1",
                    CarNumber = "1-1",
                };

                customer.CustomerVisits.AddRange(visitsReg);

                return customer;
            }
        }

        public static Customer PremiumCustomer
        {
            get
            {
                var now = DateTime.Now;
                var visitsReg = new List<Visit>()
                {
                    new Visit("1") {EnterTime = now, LeaveTime = now.AddMinutes(29) },
                    new Visit("1") {EnterTime = now, LeaveTime = now.AddHours(3).AddMinutes(12) },
                    new Visit("1") {EnterTime = now, LeaveTime = now.AddDays(2).AddHours(3).AddMinutes(12) },
                };

                var customer = new PremiumCustomer
                {
                    FirstName = "User",
                    LastName = "1",
                    CarNumber = "1",
                };

                customer.CustomerVisits.AddRange(visitsReg);

                return customer;
            }
        }
    }
}
