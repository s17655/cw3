using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Models
{
    public class Medicament
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idMedicament { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }

        public ICollection<Prescription_Medicament> prescriptions_medicaments { get; set; }
    }
}
