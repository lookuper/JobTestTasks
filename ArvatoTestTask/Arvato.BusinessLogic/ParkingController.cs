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
        public readonly List<IParkingHouse> AvaliableParking = new List<IParkingHouse>
        {
            new DefaultParingHouse("Parking 1", "Address 1", parkingPrice1),
            new DefaultParingHouse("Parking 2", "Address 2", parkingPrice1),

            // another parking with prices
        };

        public ParkingController()
        {
            var i = 5;
        }

        public void AddNewParking(IParkingHouse parking)
        {
            if (parking == null)
                throw new ArgumentNullException(nameof(parking));

            AvaliableParking.Add(parking);
        }

        public List<Invoice> GetAllInvoicesForMonth(Moths month)
        {
            return AvaliableParking.SelectMany(p => p.GetAllCustomersInvoicesForMonth(month))
                .ToList();
        }
    }
}
