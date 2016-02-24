using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.Common
{
    public abstract class Customer
    {
        public long Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String CarNumber { get; set; }

        public abstract double MonthlyFee { get; }
        public abstract double MaximumInvoice { get; }

        public bool IsPremiumCustomer { get; set; }
        public List<Visit> CustomerVisits { get; set; }
        public AbstractParkingPrices CustomerPrices { get; set; }        

        public Customer(String carNumber, String firstName = null, String lastName = null, AbstractParkingPrices prices = null)
        {
            CarNumber = carNumber;
            FirstName = firstName;
            LastName = lastName;
            CustomerPrices = prices;
            CustomerVisits = new List<Visit>();
        }

        public virtual Invoice GetInvoice(Moths forMonth)
        {
            var invoice = new Invoice();

            var monthVisits = CustomerVisits
                .Where(v => v.LeaveTime.Month == (int)forMonth)
                .ToList();

            invoice.Month = forMonth;
            invoice.Customer = $"{FirstName} {LastName}, car number: '{CarNumber}'";
            invoice.Visits = monthVisits;
            invoice.Total = monthVisits.Sum(v => v.ToPay) + MonthlyFee;

            invoice.Details = String.Join(Environment.NewLine, monthVisits.Select(v => $"{v.EnterTime} - {v.LeaveTime}, to pay: {v.ToPay} euro"));
            invoice.Details += $"{Environment.NewLine}Monthly fee: {MonthlyFee} euro";

            if (IsPremiumCustomer && invoice.Total > MaximumInvoice)
            {
                invoice.Details += $"{Environment.NewLine}Due the premium customer total price ({invoice.Total} euro) restricted up to {MaximumInvoice} euro";
                invoice.Total = MaximumInvoice;
            }

            return invoice;
        }
    }
}
