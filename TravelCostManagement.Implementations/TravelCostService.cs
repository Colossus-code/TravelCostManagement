using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCostManagement.Contracts.DomainEntities;
using TravelCostManagement.Contracts.DomainEntities.DomainResponses;
using TravelCostManagement.Contracts.RepositoryContracts;
using TravelCostManagement.Contracts.ServiceContracts;

namespace TravelCostManagement.Implementations
{
    public class TravelCostService : ITravelCostService
    {
        private readonly IRepositoryCache _repositoryCache;
        private readonly IRepositoryPlanetaryDistances _repositoryPlanetaryDistances;
        private readonly IRepositoryPlanets _repositoryPlanets;
        private readonly IRepositoryRebelPercentByPlanet _repositoryRebelPercent;
        private readonly IRepositoyPriceForLunarYears _repositoryPriceForLunarYears;

        public TravelCostService(IRepositoryCache repositoryCache, IRepositoryPlanetaryDistances repositoryPlanetsDistance, IRepositoryPlanets repositoryPlanets, IRepositoryRebelPercentByPlanet repositoryRebelPercent, IRepositoyPriceForLunarYears repositoryPriceForLunarYears)
        {
            _repositoryCache = repositoryCache;
            _repositoryPlanetaryDistances = repositoryPlanetsDistance;
            _repositoryPlanets = repositoryPlanets;
            _repositoryRebelPercent = repositoryRebelPercent;
            _repositoryPriceForLunarYears = repositoryPriceForLunarYears;

        }
        public async Task<List<PlanetaryDistancesResponse>> GetAllDistancePlanets()
        {
            var cachedResponse = _repositoryCache.GetCache<PlanetaryDistancesResponse>("planetsInfo");

            if (cachedResponse != null) return cachedResponse;

            List<Planets> planets = await _repositoryPlanets.GetPlanets();

            PlanetaryDistances planetaryDistances = await _repositoryPlanetaryDistances.GetPlanetaryDistances();

            return CallculateDistances(planets, planetaryDistances);


        }

        private List<PlanetaryDistancesResponse> CallculateDistances(List<Planets> planets, PlanetaryDistances planetaryDistances)
        {
            List<PlanetaryDistancesResponse> result = new List<PlanetaryDistancesResponse>();

            foreach (string key in planetaryDistances.PlanetDistances.Keys)
            {
                foreach (Distances distance in planetaryDistances.PlanetDistances[key])
                {
                    PlanetaryDistancesResponse planetaryDistancesResponse = new PlanetaryDistancesResponse();

                    planetaryDistancesResponse.PlanetOrigin = planets.First(e => e.ShortCode == key).PlanetName;

                    planetaryDistancesResponse.PlanetDestination = planets.First(e => e.ShortCode == distance.ShortCodePlanetName).PlanetName;

                    planetaryDistancesResponse.MoonDaysDistance += distance.Distance * 100;

                    result.Add(planetaryDistancesResponse);
                }
            }
            _repositoryCache.SetCache<PlanetaryDistancesResponse>("planetsInfo", result);
            return result;

        }

        public async Task<TotalTravelCost> GetTotalTravelCost(string entryPlanet, string exitPlanet)
        {
            var cachedTotalCost = _repositoryCache.GetCacheObject<TotalTravelCost>($"{entryPlanet}to{exitPlanet}", null);

            if (cachedTotalCost != null) return cachedTotalCost;

            List<Planets> planets = await _repositoryPlanets.GetPlanets();

            Planets planetOrigin = planets.FirstOrDefault(e => e.PlanetName == entryPlanet);

            Planets planetDestination = planets.FirstOrDefault(e => e.PlanetName == exitPlanet);

            if (planetOrigin == null || planetDestination == null) return null;

            TotalTravelCost totalTravelCost = new TotalTravelCost();   

            PriceForLunarYears priceForLunarYears = await _repositoryPriceForLunarYears.GetPriceLunarYear();
            
            decimal distanceCost = await GetDistanceBetweenPlanets(entryPlanet, exitPlanet, priceForLunarYears);

            TravelTaxes travelTaxes = await CalculateTaxes(distanceCost, entryPlanet, exitPlanet);

            totalTravelCost.TaxesCost = travelTaxes;

            totalTravelCost.TotalCost = Math.Round(travelTaxes.OriginDefenseCost + travelTaxes.DestinationDefenseCost + travelTaxes.EliteDefenseCost + distanceCost , 2);

            totalTravelCost.PricesForLunarDay = Math.Round((totalTravelCost.TotalCost / 100), 2); 

            _repositoryCache.SetCacheObject<TotalTravelCost>($"{entryPlanet}to{exitPlanet}", totalTravelCost);

            return totalTravelCost;
        }

        private async Task<decimal> GetDistanceBetweenPlanets(string entryPlanet, string exitPlanet, PriceForLunarYears priceForLunarYears)
        {
            List<PlanetaryDistancesResponse> planetaryDistances = await GetAllDistancePlanets();

            var planetaryDistance = planetaryDistances.FirstOrDefault(e => e.PlanetOrigin == entryPlanet && e.PlanetDestination == exitPlanet);

            if (planetaryDistance == null)
            {
                planetaryDistance = planetaryDistances.FirstOrDefault(e => e.PlanetOrigin == exitPlanet && e.PlanetDestination == entryPlanet);
            }

            return (planetaryDistance.MoonDaysDistance / 100) * priceForLunarYears.PriceForLunarYear;
        }

        private async Task<TravelTaxes> CalculateTaxes(decimal distanceCost, string entryPlanet, string exitPlanet)
        {
            
            List<RebelPercentByPlanet> rebelPercentByPlanets = await _repositoryRebelPercent.GetRebelPercentByPlanet();

            RebelPercentByPlanet rebelPercentOrigin = rebelPercentByPlanets.FirstOrDefault(e => e.PlanetName == entryPlanet);

            RebelPercentByPlanet rebelPercentDestination = rebelPercentByPlanets.FirstOrDefault(e => e.PlanetName == exitPlanet);

            decimal eliteCosts = 0;

            if ((rebelPercentOrigin.PercentRebel + rebelPercentDestination.PercentRebel) > 50)
            {
                eliteCosts = distanceCost * (rebelPercentDestination.PercentRebel + rebelPercentDestination.PercentRebel) / 100;
            }

            return new TravelTaxes
            {
                DestinationDefenseCost = Math.Round((distanceCost * rebelPercentDestination.PercentRebel) / 100, 2),
                EliteDefenseCost = Math.Round(eliteCosts, 2),
                OriginDefenseCost = Math.Round((distanceCost * rebelPercentOrigin.PercentRebel) /100, 2)
            };
        }
    }
}
