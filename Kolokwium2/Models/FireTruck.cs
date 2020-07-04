using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Models
{
    public class FireTruck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idFireTruck { get; set; }
        [Required]
        [MaxLength(10)]
        public string operationalNumber { get; set; }
        public bool specialEquipment { get; set; }
        public IEnumerable<FireTruckAction> FireTruckActions { get; set; }
    }
}
