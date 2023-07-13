using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCostManagement.Contracts.DomainEntities;
using TravelCostManagement.Contracts.RepositoryContracts;
using TravelCostManagement.InfrastructureData.DTOs;
using TravelCostManagement.InfrastructureData.RepositoryHelpers;

namespace TravelCostManagement.InfrastructureData
{
    public class RepositoryPlanets : IRepositoryPlanets
    {
        private readonly string _rootPath;

        public RepositoryPlanets(IConfiguration configuration)
        {
            _rootPath = configuration.GetSection("ApiCalls:Planets").Value;
        }
        public async Task<List<Planets>> GetPlanets()
        {
            List<PlanetsDto> planetsDto = await RepositoryHelper.GetList<PlanetsDto>(_rootPath);

            return TransformDtoToEntity(planetsDto);
        }

        private List<Planets> TransformDtoToEntity(List<PlanetsDto> planetsDto)
        {
            List<Planets> planetsList = new List<Planets>();

            planetsDto.ForEach(e =>
            {
                planetsList.Add(
                    
                    new Planets
                    {
                        ShortCode = e.ShortCode,
                        PlanetName = e.PlanetName,
                        Sector = e.Sector
                    });
            });

            return planetsList;
        }
    }
}
