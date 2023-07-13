using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TravelCostManagement.InfrastructureData.DTOs
{
    public class PlanetsDto
    {
        [JsonPropertyName("planetName")]
        public string PlanetName { get; set; }
        
        [JsonPropertyName("code")]
        public string ShortCode { get; set; }
        
        [JsonPropertyName("sector")]
        public string Sector { get; set; }
    }
}
