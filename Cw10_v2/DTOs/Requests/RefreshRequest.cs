using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10_v2.DTOs.Requests
{
    public class RefreshRequest : Attribute
    {
        [Required]
        public string refreshToken { get; set; }
    }
}
