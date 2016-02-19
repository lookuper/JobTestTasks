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

        void AddPremiumCustomer(String carNumber, String firstName, String lastName);
        void CarEnters(String carNumber);
        void CarLeave(String carNumber);

        void GetInvoiceForCustomer(String carNumber);
        void GetAllInvoices();
    }
}
