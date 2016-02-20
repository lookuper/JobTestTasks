using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.Common
{
    public class RegularCustomer : Customer
    {
        public override double MonthlyFee
        {
            get
            {
                return 0;
            }
        }

        public override double MaximumInvoice
        {
            get
            {
                return 0;
            }
        }

        public RegularCustomer(String carNumber, String firstName = null, String lastName = null, AbstractParkingPrices prices = null) 
            : base(carNumber, firstName, lastName, prices)
        {
            IsPremiumCustomer = false;
        }
    }
}
