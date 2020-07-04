using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test1.DTO;

namespace Test1.Services
{
    public interface IDbService
    {
        public ArtistInfoRespond getArtistInfo(int id);
        public bool UpdatePerformance(PerformanceRequest request);
    }
}
