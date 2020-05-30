using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Models
{
    public class Prescription_Medicament
    {
        [Key]
        public int idPrescription_Medicament { get; set; }

        [ForeignKey("prescription")]
        public int idPrescription { get; set; }

        [ForeignKey("medicament")]
        public int idMedicament { get; set; }

        public Prescription prescription { get; set; }
        public Medicament medicament { get; set; }
        public int? dose { get; set; }
        public string details { get; set; }
        
    }
}
