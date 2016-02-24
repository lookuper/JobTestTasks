using Arvato.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.DataAccess
{
    public class LetsSayRealDataModel
    {
        public static DateTime DefaultDateTime = new DateTime(2016, 2, 18);

        private static AbstractParkingPrices _prices = new DefaultParkingPricesForDb();
        private static Customer _regularCustomer = new RegularCustomer("r1", String.Empty, String.Empty, _prices);
        private static Customer _premiumCustomer = new PremiumCustomer("P1", "Premium", "Customer", _prices);

        public static List<Customer> AllCustomers
        {
            get
            {
                return new List<Customer>() { LetsSayRealDataModel.RegularCustomer, LetsSayRealDataModel.PremiumCustomer };
            }               
        }

        public static Customer RegularCustomer
        {
            get
            {
                var now = new DateTime(2016, 2, 18);
                var visitsReg = new List<Visit>()
                {
                    new Visit(_regularCustomer) {EnterTime = now, LeaveTime = now.AddMinutes(29) },
                    new Visit(_regularCustomer) {EnterTime = now, LeaveTime = now.AddHours(3).AddMinutes(30) },
                    new Visit(_regularCustomer) {EnterTime = now, LeaveTime = now.AddHours(3).AddMinutes(12) },
                    new Visit(_regularCustomer) {EnterTime = now, LeaveTime = now.AddDays(2).AddHours(3).AddMinutes(12) },
                };

                visitsReg.ForEach(v => v.RacalculatePrice());
                _regularCustomer.CustomerVisits.AddRange(visitsReg);
                return _regularCustomer;
            }
        }

        public static Customer PremiumCustomer
        {
            get
            {
                var now = new DateTime(2016, 2, 18);
                var visitsReg = new List<Visit>()
                {
                    new Visit(_premiumCustomer) {EnterTime = now, LeaveTime = now.AddMinutes(29) },
                    new Visit(_premiumCustomer) {EnterTime = now, LeaveTime = now.AddHours(3).AddMinutes(12) },
                    new Visit(_premiumCustomer) {EnterTime = now, LeaveTime = now.AddDays(2).AddHours(3).AddMinutes(12) },
                };

                visitsReg.ForEach(v => v.RacalculatePrice());
                _premiumCustomer.CustomerVisits.AddRange(visitsReg);
                return _premiumCustomer;
            }
        }
    }
}
