using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10_v2.DTOs.Requests
{
    public class LoginRequest : Attribute
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Haslo { get; set; }

    }
}
