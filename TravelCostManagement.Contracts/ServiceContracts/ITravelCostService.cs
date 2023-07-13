using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCostManagement.Contracts.DomainEntities.DomainResponses;

namespace TravelCostManagement.Contracts.ServiceContracts
{
    public interface ITravelCostService
    {
        Task<List<PlanetaryDistancesResponse>> GetAllDistancePlanets();
        Task<TotalTravelCost> GetTotalTravelCost(string entryPlanet, string exitPlanet);

    }
}
