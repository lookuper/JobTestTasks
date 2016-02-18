using Arvato.Common;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.BusinessLogic
{
    public class ParkingPrices : AbstractParkingPrices
    {
        public virtual double CalculateVisitPremiumCustomer(Customer cust, Visit visit)
        {
            var sameDay = TimeCompare.IsSameDay(visit.EnterTime, visit.LeaveTime.Value);

            var notBusinessBefore = new TimeRange(
                   TimeTrim.Hour(visit.EnterTime, 0),
                   TimeTrim.Hour(visit.EnterTime, BusinessHourStart));
            var businessRange = new TimeRange(
                   TimeTrim.Hour(visit.EnterTime, BusinessHourStart),
                   TimeTrim.Hour(visit.EnterTime, BusinessHourEnd));
            var afterBusinessTime = new TimeRange(
                   TimeTrim.Hour(visit.EnterTime, BusinessHourEnd),
                   TimeTrim.Hour(visit.EnterTime, 23, 59));

            //
            var minutesInside = visit.EnterTime.Subtract(visit.LeaveTime.Value).TotalMinutes;

            var fullIntervals = minutesInside / PaidIntervalInMinutes;
            var additionalInterval = minutesInside % PaidIntervalInMinutes == 0 ? 0 : 1;
            var allIntervalsToPay = fullIntervals + additionalInterval;

            if (cust.IsPremiumCustomer)
                return allIntervalsToPay * HuffAnHourPremiumPriceBusinessTime;
            else
                return allIntervalsToPay * HuffAnHourPremiumPriceNotBusinessTime;
        }
    }
}
