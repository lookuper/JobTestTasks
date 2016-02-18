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
            CurrentPrices = prices ?? AbstractParkingPrices.Default;
            foreach (var user in SimpleDataModel.AllCustomers)
            {
                AllVisits.Add(user.CarNumber, user);
            }
        }

        public void SetParkingPrices(AbstractParkingPrices prices)
        {
            CurrentPrices = prices;
        }

        public void CarEnters(String carNumber)
        {
            var visit = new Visit(carNumber);

            if (AllVisits.ContainsKey(carNumber))
                AllVisits[carNumber].CustomerVisits.Add(visit);
            else
            {
                var customer = new RegularCustomer();
                customer.CarNumber = carNumber;
                customer.CustomerVisits.Add(visit);
                // store in db if needed
                AllVisits.Add(carNumber, customer);
            }
        }

        public void CarLeave(String number)
        {
            if (!AllVisits.ContainsKey(number))
                throw new InvalidOperationException("Such car dont entered in parking");

            var lastVisit = AllVisits[number].CustomerVisits.FirstOrDefault(v => v.LeaveTime == null);
            if (lastVisit == null)
                throw new InvalidOperationException("not properly entered car try to leave");

            lastVisit.CarLeave();
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
