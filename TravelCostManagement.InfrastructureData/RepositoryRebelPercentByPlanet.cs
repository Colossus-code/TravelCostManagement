using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TravelCostManagement.Contracts.DomainEntities;
using TravelCostManagement.Contracts.RepositoryContracts;
using TravelCostManagement.InfrastructureData.DTOs;
using TravelCostManagement.InfrastructureData.RepositoryHelpers;

namespace TravelCostManagement.InfrastructureData
{
    public class RepositoryRebelPercentByPlanet : IRepositoryRebelPercentByPlanet
    {
        private readonly string _rootPath;

        public RepositoryRebelPercentByPlanet(IConfiguration configuration)
        {
            _rootPath = configuration.GetSection("ApiCalls:RebelPercent").Value;
        }

        public async Task<List<RebelPercentByPlanet>> GetRebelPercentByPlanet()
        {
            List<RebelPercentByPlanetDto> rebelPercentByPlanetDto = await RepositoryHelper.GetList<RebelPercentByPlanetDto>(_rootPath);

            return TransformDtoToEntity(rebelPercentByPlanetDto);
        }

        private List<RebelPercentByPlanet> TransformDtoToEntity(List<RebelPercentByPlanetDto> rebelPercentByPlanetDto)
        {
            List<RebelPercentByPlanet> rebelPercentByPlanets = new List<RebelPercentByPlanet>();

            foreach(var rebelPercent in rebelPercentByPlanetDto)
            {
                rebelPercentByPlanets.Add(
                    
                    new RebelPercentByPlanet
                    {
                        PlanetName = rebelPercent.PlanetName,
                        PercentRebel = int.Parse(rebelPercent.PercentRebel)
                    });
            }

            return rebelPercentByPlanets;
        }
    }
}
