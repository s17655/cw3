using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Models
{
    public class FirefighterAction
    {
        [ForeignKey("firefighter")]
        public int idFirefighter { get; set; }
        [ForeignKey("action")]
        public int idAction { get; set; }
        public Firefighter firefighter { get; set; }
        public Action action { get; set; }

    }
}
