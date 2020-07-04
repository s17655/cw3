using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Models
{
    public class Action
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idAction{ get; set; }
        public DateTime startDate { get; set; }
        [AllowNull]
        public DateTime endDate { get; set; }
        public bool needSpecialEquipment { get; set; }
        public IEnumerable<FirefighterAction> firefighterActions { get; set; }
        public IEnumerable<FireTruckAction> FireTruckActions { get; set; }

    }
}
