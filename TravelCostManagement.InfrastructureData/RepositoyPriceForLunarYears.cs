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
    public class RepositoyPriceForLunarYears : IRepositoyPriceForLunarYears
    {
        private readonly string _rootPath;

        public RepositoyPriceForLunarYears(IConfiguration configuration)
        {
            _rootPath = configuration.GetSection("ApiCalls:Price").Value;
        }
        public async Task<PriceForLunarYears> GetPriceLunarYear()
        {
            PriceForLunarYearsDto priceForLunarYearsDto = await RepositoryHelper.GetObject<PriceForLunarYearsDto>(_rootPath,null);

            return TransformDtoToEntity(priceForLunarYearsDto);
        }

        private PriceForLunarYears TransformDtoToEntity(PriceForLunarYearsDto priceForLunarYearsDto)
        {
            return new PriceForLunarYears
            {
                PriceForLunarYear = priceForLunarYearsDto.PrieceForLunarYear
            };
        }
    }
}
