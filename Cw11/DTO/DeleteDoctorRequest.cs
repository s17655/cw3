﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.DTO
{
    public class DeleteDoctorRequest :Attribute
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }

    }
}
