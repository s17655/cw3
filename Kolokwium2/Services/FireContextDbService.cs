using Kolokwium2.DTO;
using Kolokwium2.Exceptions;
using Kolokwium2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Kolokwium2.Services
{
    public class FireContextDbService: IDbService
    {
        private readonly FireDbContext _context;
        public FireContextDbService(FireDbContext context)
        {
            _context = context;
        }

        public ICollection<ActionResponse> getActions(int id)
        {
            if (!(_context.Firefighters.Any(x => x.idFirefighter == id)))
            {
                return null;
            }

            List<ActionResponse> actions = new List<ActionResponse>();
            actions = (from tab1 in _context.Actions
                       join tab2 in _context.FirefighterActions on tab1.idAction equals tab2.idAction
                       where tab2.idFirefighter == id
                       orderby tab1.endDate descending
                       select new ActionResponse
                        {
                           idAction=tab1.idAction,
                           startTime=tab1.startDate,
                           endTime=tab1.endDate
                       }).ToList();

            return actions;
        }


        public bool signFireTruckForAction(FireTruckSignRequest request)
        {
            FireTruck truck = _context.FireTrucks.Where(x => x.idFireTruck == request.idFireTruck).FirstOrDefault();
            Models.Action action = _context.Actions.Where(x => x.idAction == request.idAction).FirstOrDefault();

            if (truck == null)
            {
                throw new FireTruckNotFoundException("Nie znaleziono podanego wozu");
            }

            if (action == null)
            {
                throw new ActionNotFoundException("Nie znaleziono podanej akcji");
            }

          var actionList = (from tab1 in _context.Actions
                           join tab2 in _context.FireTruckActions on tab1.idAction equals tab2.idAction
                           where tab2.idFireTruck == request.idFireTruck
                           select new 
                           {
                               startTime = tab1.startDate,
                               endTime = tab1.endDate,
                               needSpecialEquipment = tab1.needSpecialEquipment,
                           }).ToList();

            foreach(var x in actionList)
            {
                if((DateTime.Now>x.startTime&& DateTime.Now < x.endTime)|| (DateTime.Now > x.startTime && x.endTime==null))
                {
                    throw new FireTruckIsOccupiedException("Podany woz jest obecnie zajety");
                }
            }

            if (action.needSpecialEquipment && !(truck.specialEquipment))
            {
                throw new NeedSpecialEquipmentException("Podany woz nie posiada potrzebnego wyposazenia");
            }


            _context.FireTruckActions.Add(new FireTruckAction
            {
                idFireTruck = request.idFireTruck,
                idAction = request.idAction,
                assignmentDate = DateTime.Now
            });
            _context.SaveChanges();
            return true;
        }




    }
}
