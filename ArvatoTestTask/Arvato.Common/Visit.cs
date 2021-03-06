﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.Common
{
    public class Visit
    {
        public Customer Customer { get; set; }
        public String CarNumber { get; set; }
        public DateTime EnterTime { get; set; }
        public DateTime LeaveTime { get; set; }
        public double ToPay { get; set; }
        public List<String> Log { get; set; }

        [Obsolete("Just for testing purposes")]
        public Visit() : this(null)
        {

        }

        public Visit(Customer customer)
        {
            Customer = customer;
            EnterTime = DateTime.Now;
            LeaveTime = DateTime.MinValue;
            ToPay = 0;
            Log = new List<string>();
        }

        public void CarLeave()
        {
            LeaveTime = DateTime.Now;
            ToPay = Customer.CustomerPrices.CalculateVisitPrice(Customer, this);
        }

        public void RacalculatePrice()
        {
            if (EnterTime == DateTime.MinValue || LeaveTime == DateTime.MinValue)
                throw new InvalidOperationException("Cannot calculate price");

            ToPay = Customer.CustomerPrices.CalculateVisitPrice(Customer, this);
        }

        public override string ToString()
        {
            return String.Join(Environment.NewLine, Log);
        }
    }
}
