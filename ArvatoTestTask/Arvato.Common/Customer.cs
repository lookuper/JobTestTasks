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
        public List<Visit> CustomerVisits { get; protected set; }

        public Customer()
        {
            CustomerVisits = new List<Visit>();
        }
    }
}
