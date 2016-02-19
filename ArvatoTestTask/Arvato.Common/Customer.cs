using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.Common
{
    public abstract class Customer
    {
        public long Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String CarNumber { get; set; }

        public bool IsPremiumCustomer { get; protected set; }
        public List<Visit> CustomerVisits { get; set; }
        public AbstractParkingPrices CustomerPrices { get; set; }

        public Customer(String carNumber, String firstName = null, String lastName = null, AbstractParkingPrices prices = null)
        {
            CarNumber = carNumber;
            FirstName = firstName;
            LastName = lastName;
            CustomerPrices = prices;
            CustomerVisits = new List<Visit>();
        }
    }
}
