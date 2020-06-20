using Kolokwium.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium.Services
{
    public interface IDbService
    {
        public IEnumerable<ArtistReposnd> getArtistInfo(int id);
        

    }
}
