using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Test1.Models
{
    public class Organiser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idOrganiser { get; set; }
        [Required]
        [MaxLength(30)]
        public string name { get; set; }
        public ICollection<EventOrganiser> eventOrganisers { get; set; }
    }
}
