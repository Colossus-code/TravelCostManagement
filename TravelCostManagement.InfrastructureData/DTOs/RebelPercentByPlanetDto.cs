using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TravelCostManagement.InfrastructureData.DTOs
{
    public class RebelPercentByPlanetDto
    {
        [JsonPropertyName("planetName")]
        public string PlanetName { get; set; }

        [JsonPropertyName("rebelPercent")]
        public string PercentRebel { get; set; }
    }
}
