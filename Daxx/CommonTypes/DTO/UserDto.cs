using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public bool AgreeToWorkForFood { get; set; }
        public CountryDto Location { get; set; }
    }
}
