using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.DTO
{
    public class FireTruckSignRequest:Attribute
    {
        [Required]
        public int idAction { get; set; }
        [Required]
        public int idFireTruck { get; set; }
    }
}
