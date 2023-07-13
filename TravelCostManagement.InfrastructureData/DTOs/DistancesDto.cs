using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TravelCostManagement.InfrastructureData.DTOs
{
    public class DistancesDto
    {
        [JsonPropertyName("code")]
        public string ShortCodePlanetName { get; set; }

        [JsonPropertyName("lunarYears")]
        public decimal Distance { get; set; }
    }
}
