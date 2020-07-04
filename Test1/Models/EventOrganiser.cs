using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Test1.Models
{
    public class EventOrganiser
    {
        [ForeignKey("event_")]
        public int idEvent { get; set; }
        [ForeignKey("organiser")]
        public int idOrganiser { get; set; }
        public Event event_ { get; set; }
        public Organiser organiser { get; set; }
    }
}
