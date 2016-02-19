using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.Common
{
    public abstract class AbstractParkingPrices
    {
        public double MontleyFeeRegular { get; protected set; }
        public double MontleyFeePremium { get; protected set; }

        public double HuffAnHourdRegularPriceBusinessTime { get; protected set; }
        public double HuffAndHourRegularPriceNotBusinessTime { get; protected set; }

        public double HuffAnHourPremiumPriceBusinessTime { get; protected set; }
        public double HuffAnHourPremiumPriceNotBusinessTime { get; protected set; }

        public int PaidIntervalInMinutes { get; protected set; }

        public int BusinessHourStart { get; protected set; }
        public int BusinessHourEnd { get; protected set; }

        public AbstractParkingPrices()
        {
            MontleyFeeRegular = 0.0;
            MontleyFeePremium = 20.0;

            HuffAnHourdRegularPriceBusinessTime = 1.50;
            HuffAndHourRegularPriceNotBusinessTime = 1.00;

            HuffAnHourPremiumPriceBusinessTime = 1.0;
            HuffAnHourPremiumPriceNotBusinessTime = 0.75;

            PaidIntervalInMinutes = 30;

            BusinessHourStart = 7; //07:00
            BusinessHourEnd = 19; // 19:00
        }

        public abstract double CalculateVisitPrice(Customer customer, Visit visit);
    }
}
