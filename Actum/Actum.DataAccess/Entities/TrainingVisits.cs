using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actum.DataAccess.Entities
{
    public class TrainingVisit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        public Employee Employee { get; set; }

        [Required]
        public Training Training { get; set; }

        [Required]
        public DateTime When { get; set; }
    }
}
