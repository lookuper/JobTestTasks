using Arvato.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.BusinessLogic
{
    public class ParkingController
    {
        private static AbstractParkingPrices parkingPrice1 = new ParkingPrices();
        private readonly List<IParkingHouse> avaliableParking = new List<IParkingHouse>
        {
            new DefaultParingHouse(parkingPrice1),
            // another parking with prices
        };

        public void AddNewParking(IParkingHouse parking)
        {
            if (parking == null)
                throw new ArgumentNullException(nameof(parking));

            avaliableParking.Add(parking);
        }

        public List<Invoice> GetAllInvoicesForMonth(Moths month)
        {
            return avaliableParking.SelectMany(p => p.GetAllCustomersInvoicesForMonth(month))
                .ToList();
        }
    }
}
