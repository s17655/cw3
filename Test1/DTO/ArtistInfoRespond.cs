using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test1.Models;

namespace Test1.DTO
{
    public class ArtistInfoRespond:Attribute
    {
        public string nickname { get; set; }
        public IEnumerable<Event>  events { get; set; }
    }
}
