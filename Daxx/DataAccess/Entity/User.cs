using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        public String Login { get; set; }

        [Required]
        public String Password { get; set; }

        [Required]
        public bool AgreeToWorkForFood { get; set; }
        public Country Location { get; set; }

        public User()
        {

        }
    }
}
