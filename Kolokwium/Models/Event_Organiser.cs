using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium.Models
{
    public class Event_Organiser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idEventOrganiser { get; set; }

        [ForeignKey("organiser")]
        public int idOrganiser { get; set; }
        [ForeignKey("event_")]
        public int idEvent { get; set; }
        public Organiser organiser { get; set; }
        public Event event_ { get; set; }

    }
}
