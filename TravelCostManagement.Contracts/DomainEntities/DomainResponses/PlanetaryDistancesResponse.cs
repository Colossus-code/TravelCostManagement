using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCostManagement.Contracts.DomainEntities.DomainResponses
{
    public class PlanetaryDistancesResponse
    {
        public string PlanetOrigin { get; set; }
        public string PlanetDestination { get; set; }
        public decimal MoonDaysDistance { get; set; }
    }
}
