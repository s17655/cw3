using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test1.DTO
{
    public class PerformanceRequest:Attribute
    {
        [Required]
        public int idArtist { get; set; }
        [Required]
        public int idEvent { get; set; }
        [Required]
        public DateTime performanceDate { get; set; }


    }
}
