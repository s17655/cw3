using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Models
{
    public class FireTruckAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idFireTruckAction { get; set; }
        [ForeignKey("FireTrucks")]
        public int idFireTruck { get; set; }
        [ForeignKey("Actions")]
        public int idAction { get; set; }
        public DateTime assignmentDate { get; set; }
        public Action Actions { get; set; }
        public FireTruck FireTrucks { get; set; }
    }
}
