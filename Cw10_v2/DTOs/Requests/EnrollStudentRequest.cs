using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10_v2.DTOs.Requests
{
    public class EnrollStudentRequest : Attribute
    {
        [Required]
        public string IndexNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string BirthDate { get; set; }
        [Required]
        public string Studies { get; set; }
    }
}
