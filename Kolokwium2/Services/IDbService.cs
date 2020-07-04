using Kolokwium2.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Services
{
    public interface IDbService
    {
        public ICollection<ActionResponse> getActions(int id);
        public bool signFireTruckForAction(FireTruckSignRequest request);
    }
}
