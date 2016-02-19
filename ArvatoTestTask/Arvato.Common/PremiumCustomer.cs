using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.Common
{
    public class PremiumCustomer : Customer
    {
        public PremiumCustomer(String carNumber, String firstName = null, String lastName = null, AbstractParkingPrices prices = null)
            : base(carNumber, firstName, lastName, prices)
        {
            IsPremiumCustomer = true;
        }
    }
}
