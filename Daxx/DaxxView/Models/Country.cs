using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaxxView.Models
{
    public class Country
    {
        public int Id { get; private set; }

        public string Name { get; set; }

        public virtual ICollection<Province> Provinces { get; set; }
    }
}
