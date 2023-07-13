
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TravelCostManagement.Contracts.DomainEntities.DomainResponses;
using TravelCostManagement.Contracts.ServiceContracts;
using TravelCostManagement.WebApi.Models;
using TravelCostManagement.WebApi.Models.ModelsResponse;

namespace TravelCostManagement.WebApi.Controllers
{
    [Route("TravelCostManagementController/")]
    [ApiController]
    public class TravelCostManagementController : ControllerBase
    {
        private readonly ITravelCostService _travelCostService;

        private readonly ILogger<TravelCostManagementController> _logger;


        public TravelCostManagementController(ITravelCostService travelCostService, ILogger<TravelCostManagementController> logger)
        {
            _travelCostService = travelCostService;
            _logger = logger;

        }

        [HttpGet]
        [Route("GetPlanetsDistance")]
        public async Task<IActionResult> GetPlanetsDistance()
        {
            try
            {
                List<PlanetaryDistancesModel> planetaryDistancesModel = new List<PlanetaryDistancesModel>();

                List<PlanetaryDistancesResponse> planetaryDistances = await _travelCostService.GetAllDistancePlanets();

                planetaryDistances.ForEach(planetaryDistance =>
                {
                    planetaryDistancesModel.Add(new PlanetaryDistancesModel
                    {
                        PlanetOrigin = planetaryDistance.PlanetOrigin,
                        PlanetDestination = planetaryDistance.PlanetDestination,
                        MoonDaysDistance = planetaryDistance.MoonDaysDistance
                    });
                });

                return Ok(planetaryDistances);

        }
            catch (Exception ex)
            {
                _logger.LogError($"Exception happened: {ex.Message}");
                return BadRequest("Unexpected error happened.");

    }

}

        [HttpPost]
        [Route("GetCostForTravel")]
        public async Task<IActionResult> GetCostForTravel([Required] PlanetsModels planetModels)
        {
            try
            {
                TotalTravelCost totalTravelCost = await _travelCostService.GetTotalTravelCost(planetModels.EntryPlanet, planetModels.ExitPlanet);

                if(totalTravelCost == null)
                {
                    _logger.LogError($"Introduced planets not found {planetModels.EntryPlanet},{planetModels.ExitPlanet}.");
                    return BadRequest($"Not found costs for {planetModels.EntryPlanet} and {planetModels.ExitPlanet}");

                }

                TotalTravelCostModel totalTravelCostModel = new TotalTravelCostModel()
                {
                    PricesForLunarDay = totalTravelCost.PricesForLunarDay,
                    TotalCost = totalTravelCost.TotalCost,
                    TaxesCost = new TaxesModel()
                    {
                        DestinationDefenseCost = totalTravelCost.TaxesCost.DestinationDefenseCost,
                        OriginDefenseCost = totalTravelCost.TaxesCost.OriginDefenseCost,
                        EliteDefenseCost = totalTravelCost.TaxesCost.EliteDefenseCost
                    }
                };

                return Ok(totalTravelCostModel);


            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception happened: {ex.Message}");
                return BadRequest("Unexpected error happened.");
            }
        }

    }
}
