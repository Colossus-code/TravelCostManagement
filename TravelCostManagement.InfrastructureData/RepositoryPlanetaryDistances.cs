using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCostManagement.Contracts.DomainEntities;
using TravelCostManagement.Contracts.DomainEntities.DomainResponses;
using TravelCostManagement.Contracts.RepositoryContracts;
using TravelCostManagement.InfrastructureData.DTOs;
using TravelCostManagement.InfrastructureData.RepositoryHelpers;

namespace TravelCostManagement.InfrastructureData
{
    public class RepositoryPlanetaryDistances : IRepositoryPlanetaryDistances
    {
        private readonly string _rootPath;

        public RepositoryPlanetaryDistances(IConfiguration configuration)
        {
            _rootPath = configuration.GetSection("ApiCalls:DistancePlanets").Value;
        }

        public async Task<PlanetaryDistances> GetPlanetaryDistances()
        {
            PlanetaryDistancesDto planetaryDistancesDto = new PlanetaryDistancesDto
            {
                PlanetDistances = await RepositoryHelper.GetObject<Dictionary<string, List<DistancesDto>>>(_rootPath, null)
            };

            return TransformDtoToEntity(planetaryDistancesDto);
        }

        private PlanetaryDistances TransformDtoToEntity(PlanetaryDistancesDto planetaryDistancesDto)
        {
            PlanetaryDistances planetaryDistances = new PlanetaryDistances();

            planetaryDistances.PlanetDistances = new Dictionary<string, List<Distances>>();
            
            foreach (string key in planetaryDistancesDto.PlanetDistances.Keys)
            {
                List<Distances> distances = new List<Distances>();
                
                foreach(DistancesDto distancesDto in planetaryDistancesDto.PlanetDistances[key])
                {
                    distances.Add(new Distances
                    {
                        ShortCodePlanetName = distancesDto.ShortCodePlanetName,
                        Distance = distancesDto.Distance,
                    });
                 
                }

                planetaryDistances.PlanetDistances.Add(key, distances);
            }

            return planetaryDistances; 
        }
    }
}
