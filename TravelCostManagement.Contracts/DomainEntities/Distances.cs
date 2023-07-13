using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TravelCostManagement.Contracts.DomainEntities
{
    public class Distances
    {
        public string ShortCodePlanetName { get; set; }
        public decimal Distance { get; set; }
    }
}
