using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes.DTO
{
    public class ProvinceDto
    {
        public int Id { get; private set; }
        public int CountryId { get; private set; }
        public string Name { get; private set; }
    }
}
