using Kolokwium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium.DTO
{
    public class ArtistReposnd
    {
        public int idArtist { get; set; }
        public string nickname { get; set; }
        public DateTime performanceDate { get; set; }

        public string name { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endDate { get; set; }
    }
}
