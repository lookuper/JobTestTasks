using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.Common
{
    public interface IParkingHouse
    {
        AbstractParkingPrices CurrentPrices { get; }

        void SetParkingPrices(AbstractParkingPrices prices);

        void CarEnters(String carNumber);
        void CarLeave(String number);

        void GetInvoiceForCustomer(String carNumber);
        void GetAllInvoices();
    }
}
