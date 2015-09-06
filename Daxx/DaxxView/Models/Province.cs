using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaxxView.Models
{
    public class Province
    {
        public int Id { get; private set; }

        public int CountryId { get; set; }

        public string Name { get; set; }
    }
}
