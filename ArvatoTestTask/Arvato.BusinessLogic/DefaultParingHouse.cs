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

        public string Name { get; set; }
        public string Address { get; set; }

        public Dictionary<String, Customer> AllVisits = new Dictionary<string, Customer>();

        public DefaultParingHouse(String parkingName, String parkingAddress, AbstractParkingPrices prices = null)
        {
            Name = parkingName;
            Address = parkingAddress;
            CurrentPrices = prices;
            foreach (var user in LetsSayRealDataModel.AllCustomers)
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
            if (AllVisits.ContainsKey(carNumber) && !AllVisits[carNumber].IsPremiumCustomer)
                throw new InvalidOperationException("Such user already in database and its not a premium user, ability migration from regular to premium user would be avalibel in next version");

            if (AllVisits.ContainsKey(carNumber))
                throw new InvalidOperationException("Such user already registered");

            var newCustomer = new PremiumCustomer(carNumber, firstName, lastName, CurrentPrices);
            AllVisits.Add(carNumber, newCustomer);
            // store in db

        }

        public void CarEnters(String carNumber)
        {
            Customer customer;
            if (!AllVisits.ContainsKey(carNumber))
            {
                customer = new RegularCustomer(carNumber, null, null, CurrentPrices);
                AllVisits.Add(carNumber, customer);
            }
            else
                customer = AllVisits[carNumber];                

            if (customer.CustomerVisits.Any(v => v.LeaveTime == DateTime.MinValue))
                throw new InvalidOperationException("Car already present inside parking");

            var visit = new Visit(customer);
            customer.CustomerVisits.Add(visit);
        }

        public Visit CarLeave(String number)
        {
            if (!AllVisits.ContainsKey(number))
                throw new InvalidOperationException("Such car dont entered parking");

            var customer = AllVisits[number];
            var currentVisit = customer.CustomerVisits.FirstOrDefault(v => v.LeaveTime == DateTime.MinValue);
            if (currentVisit == null)
                throw new InvalidOperationException("not properly entered car try to leave");

            currentVisit.CarLeave();
            currentVisit.ToPay = CurrentPrices.CalculateVisitPrice(customer, currentVisit);

            return currentVisit;
        }

        public List<Customer> GetAllCustomers()
        {
            return AllVisits.Values.ToList();
        }

        public List<Visit> GetCustomerVisits(String carNumber)
        {
            if (!AllVisits.ContainsKey(carNumber))
                throw new InvalidOperationException("Cannot find such car in database");

            var customer = AllVisits[carNumber];
            return customer.CustomerVisits;
        }

        public List<Invoice> GetAllCustomersInvoicesForMonth(Moths month)
        {
            var customersInMonth = AllVisits.Values
                .Where(c => c.CustomerVisits.Last().LeaveTime.Month == (int)month)
                .ToList();

            return customersInMonth.Select(c => c.GetInvoice(month)).ToList();
        }
    }
}
