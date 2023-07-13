using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCostManagement.Contracts.DomainEntities
{
    public class PlanetaryDistances
    {
        public Dictionary<string, List<Distances>> PlanetDistances { get; set; }
    }
}
