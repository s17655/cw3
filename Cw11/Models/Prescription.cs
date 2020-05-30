using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Models
{
    public class Prescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idPrescription { get; set; }
        public DateTime date { get; set; }
        public DateTime dueDate { get; set; }
        public Patient patient { get; set; }
        public Doctor doctor { get; set; }
        public ICollection<Prescription_Medicament> prescription_medicaments { get; set; }
    }
}
