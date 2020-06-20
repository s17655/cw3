using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium.Models
{

    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idEvent { get; set; }
        public string name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public ICollection<Artist_Event> artistEvents { get; set; }
        public ICollection<Event_Organiser> eventOrganisers { get; set; }

    }
}
