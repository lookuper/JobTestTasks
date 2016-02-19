using Arvato.Common;
using Arvato.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.BusinessLogic
{
    public class DefaultParingHouse : IParkingHouse
    {
        public AbstractParkingPrices CurrentPrices { get; private set; }
        public Dictionary<String, Customer> AllVisits = new Dictionary<string, Customer>();

        public DefaultParingHouse(ParkingPrices prices = null)
        {
            CurrentPrices = prices ?? new ParkingPrices();
            foreach (var user in SimpleDataModel.AllCustomers)
            {
                AllVisits.Add(user.CarNumber, user);
            }
        }

        public void SetParkingPrices(AbstractParkingPrices prices)
        {
            CurrentPrices = prices;
        }

        public void AddPremiumCustomer(String carNumber, String firstName, String lastName)
        {
            var newCustomer = new PremiumCustomer(carNumber, firstName, lastName, CurrentPrices);
            AllVisits.Add(carNumber, newCustomer);
            // store in db
        }

        public void CarEnters(String carNumber)
        {
            var customer = AllVisits.ContainsKey(carNumber) ? AllVisits[carNumber] : new RegularCustomer(carNumber, null, null, CurrentPrices);
            var visit = new Visit(customer);
            customer.CustomerVisits.Add(visit);
            //AllVisits.Add(carNumber, customer);
        }

        public void CarLeave(String number)
        {
            if (!AllVisits.ContainsKey(number))
                throw new InvalidOperationException("Such car dont entered parking");

            var customer = AllVisits[number];
            var lastVisit = customer.CustomerVisits.FirstOrDefault(v => v.LeaveTime == null);
            if (lastVisit == null)
                throw new InvalidOperationException("not properly entered car try to leave");

            lastVisit.CarLeave();
            CurrentPrices.CalculateVisitPrice(customer, lastVisit);
        }

        public void GetInvoiceForCustomer(String carNumber)
        {
            var customer = AllVisits[carNumber];



            var visits = AllVisits[carNumber];

        }

        public void GetAllInvoices()
        { }
    }
}
