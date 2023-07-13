using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCostManagement.Contracts.DomainEntities;

namespace TravelCostManagement.Contracts.RepositoryContracts
{
    public interface IRepositoyPriceForLunarYears
    {
        Task<PriceForLunarYears> GetPriceLunarYear();
    }
}
