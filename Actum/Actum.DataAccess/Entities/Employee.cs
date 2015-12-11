using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actum.DataAccess.Entities
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Surname { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
