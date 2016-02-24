using Arvato.Common;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.DataAccess
{
    public class DefaultParkingPricesForDb : AbstractParkingPrices
    {
        public virtual TimeRange GetBeforeBusinessTime(Visit visit)
        {
            return new TimeRange(
                   TimeTrim.Hour(visit.EnterTime, 0),
                   TimeTrim.Hour(visit.EnterTime, BusinessHourStart));
        }

        public virtual TimeRange GetAfterBusinessTime(Visit visit)
        {
            return new TimeRange(
                   TimeTrim.Hour(visit.EnterTime, BusinessHourEnd),
                   TimeTrim.Hour(visit.EnterTime, 23, 59));
        }

        public virtual TimeRange GetBusinessTimeRange(Visit visit)
        {
            return new TimeRange(
                   TimeTrim.Hour(visit.EnterTime, BusinessHourStart),
                   TimeTrim.Hour(visit.EnterTime, BusinessHourEnd));
        }


        public override double CalculateVisitPrice(Customer customer, Visit visit)
        {
            visit.Log.Add($"Starting calculation for customer '{customer.FirstName} {customer.LastName}' with car number: {customer.CarNumber}");
            var priceBusinessTime = customer.IsPremiumCustomer ? HuffAnHourPremiumPriceBusinessTime : HuffAnHourdRegularPriceBusinessTime;
            var priceNotBusinessTime = customer.IsPremiumCustomer ? HuffAnHourPremiumPriceNotBusinessTime : HuffAndHourRegularPriceNotBusinessTime;

            var minutesInside = visit.LeaveTime.Subtract(visit.EnterTime).TotalMinutes;
            var visitTimeRange = new TimeRange(visit.EnterTime, visit.LeaveTime);
            var sameDay = TimeCompare.IsSameDay(visit.EnterTime, visit.LeaveTime);

            visit.Log.Add($"Car enter: {visit.EnterTime}, car leave: {visit.LeaveTime}");
            visit.Log.Add($"Paid interval (minutes): {customer.CustomerPrices.PaidIntervalInMinutes}");
            visit.Log.Add($"Prices: business time ({customer.CustomerPrices.BusinessHourStart} - {customer.CustomerPrices.BusinessHourEnd}) - {priceBusinessTime} euro, other time - {priceNotBusinessTime} euro");

            if (GetBusinessTimeRange(visit).HasInside(visitTimeRange))
            {
                var price = CalculatePaidRangeSimple(minutesInside) * priceBusinessTime;
                visit.ToPay = price;
                visit.Log.Add($"Stay time: {visitTimeRange} in business time, to pay: {price} euro");
                return price;
            }

            if (sameDay &&
                   (GetBeforeBusinessTime(visit).HasInside(visitTimeRange) ||
                         GetAfterBusinessTime(visit).HasInside(visitTimeRange)))
            {
                var price = CalculatePaidRangeSimple(minutesInside) * priceNotBusinessTime;
                visit.ToPay = price;
                visit.Log.Add($"Stay time: {visitTimeRange.Duration} in not business time, to pay: {price} euro");
                return price;
            }

            var timePointer = visitTimeRange.Start;
            int bClicks = 0;
            int nbClicks = 0;

            while (timePointer < visitTimeRange.End)
            {
                if (IsTimeInBusinessTime(timePointer))
                {
                    bClicks++;
                    Console.WriteLine("Pay for premium: " + timePointer.ToShortTimeString());
                }
                else
                {
                    nbClicks++;
                    Console.WriteLine("Pay for regular: " + timePointer.ToShortTimeString());
                }

                timePointer = timePointer.AddMinutes(PaidIntervalInMinutes);
            }
            var businessPartPrice = bClicks * priceBusinessTime;
            var notBusinessPartPrice = nbClicks * priceNotBusinessTime;
            var total = businessPartPrice + notBusinessPartPrice;

            var finalMessage = $"Stay time: {visitTimeRange}, business time: {businessPartPrice}, not business time: {notBusinessPartPrice}, TOTAL: {total}";
            visit.Log.Add(finalMessage);
            return total;
        }

        protected virtual int CalculatePaidRangeSimple(double minutesInside)
        {
            var fullIntervals = (int)(minutesInside / PaidIntervalInMinutes);
            var additionalInterval = minutesInside % PaidIntervalInMinutes == 0 ? 0 : 1;
            var allIntervalsToPay = fullIntervals + additionalInterval;

            return allIntervalsToPay;
        }

        protected virtual bool IsTimeInBusinessTime(DateTime time)
        {
            var bRange = new TimeRange(
                         TimeTrim.Hour(time, BusinessHourStart),
                         TimeTrim.Hour(time, BusinessHourEnd));

            return bRange.HasInside(time);
        }
    }
}
