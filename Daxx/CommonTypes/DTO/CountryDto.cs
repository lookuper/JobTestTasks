using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes.DTO
{
    public class CountryDto
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<ProvinceDto> Provinces { get; private set; }
    }
}
