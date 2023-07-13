using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TravelCostManagement.InfrastructureData.DTOs
{
    public class PlanetaryDistancesDto { 
  
        public Dictionary<string, List<DistancesDto>> PlanetDistances { get; set; }
    }
}
