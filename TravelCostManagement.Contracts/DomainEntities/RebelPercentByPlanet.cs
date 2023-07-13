using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TravelCostManagement.Contracts.DomainEntities
{
    public class RebelPercentByPlanet
    {

        public string PlanetName { get; set; }

        public int PercentRebel { get; set; }
    }
}
