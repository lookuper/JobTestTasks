using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.Common
{
    public class Invoice
    {
        public String Customer { get; set; }
        public Moths Month { get; set; }
        public List<Visit> Visits { get; set; }
        public String Details { get; set; }
        public double Total { get; set; }
    }
}
