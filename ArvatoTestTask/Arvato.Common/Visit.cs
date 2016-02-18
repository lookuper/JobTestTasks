using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.Common
{
    public class Visit
    {
        public String CarNumber { get; set; }
        public DateTime EnterTime { get; set; }
        public DateTime? LeaveTime { get; set; }

        public Visit(String carNumber)
        {
            CarNumber = carNumber;
            EnterTime = DateTime.Now;
            LeaveTime = null;
        }

        public void CarLeave()
        {
            LeaveTime = DateTime.Now;
        }

        public int CalculateVisitTimeInMinutes()
        {
            if (LeaveTime == null)
                throw new InvalidOperationException("Cannot calculate time because car still not leave parking");

            var time = LeaveTime.Value.Subtract(EnterTime);
            return time.Minutes;
        }
    }
}
