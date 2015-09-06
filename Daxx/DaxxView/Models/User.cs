using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaxxView.Models
{
    public class User
    {
        public int Id { get; private set; }

        public String Login { get; set; }

        public String Password { get; set; }

        public bool AgreeToWorkForFood { get; set; }

        public Country Location { get; set; }
    }
}
