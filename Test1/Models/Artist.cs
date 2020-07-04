using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Test1.Models
{
    public class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idArtist { get; set; }
        [Required]
        [MaxLength(30)]
        public string nickname { get; set; }
        public ICollection<ArtistEvent> artistEvents { get; set; }
    }
}
