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
        public static DateTime StartTimeBusiness
        {
            get
            {
                return new DateTime(2016, 2, 18).AddHours(11);
            }
        }

        public static DateTime StartTimeRegular
        {
            get
            {
                return new DateTime(2016, 2, 18).AddHours(20);
            }
        }

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
                var now = new DateTime(2016, 2, 18);
                var visitsReg = new List<Visit>()
                {
                    new Visit() {EnterTime = now, LeaveTime = now.AddMinutes(29) },
                    new Visit() {EnterTime = StartTimeRegular, LeaveTime = StartTimeRegular.AddHours(3).AddMinutes(30) },
                    new Visit() {EnterTime = StartTimeBusiness, LeaveTime = StartTimeBusiness.AddHours(3).AddMinutes(12) },
                    new Visit() {EnterTime = StartTimeBusiness, LeaveTime = StartTimeBusiness.AddDays(2).AddHours(3).AddMinutes(12) },
                };

                var customer = new RegularCustomer(carNumber: "1-1");

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
                    new Visit() {EnterTime = now, LeaveTime = now.AddMinutes(29) },
                    new Visit() {EnterTime = now, LeaveTime = now.AddHours(3).AddMinutes(12) },
                    new Visit() {EnterTime = now, LeaveTime = now.AddDays(2).AddHours(3).AddMinutes(12) },
                };

                var customer = new PremiumCustomer("1");               

                customer.CustomerVisits.AddRange(visitsReg);

                return customer;
            }
        }
    }
}
