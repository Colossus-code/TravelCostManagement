using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCostManagement.Contracts.DomainEntities.DomainResponses
{
    public class TotalTravelCost
    {
        public decimal TotalCost { get; set; }

        public decimal PricesForLunarDay { get; set; }

        public TravelTaxes TaxesCost { get; set; }
    }
    public class TravelTaxes
    {
        public decimal OriginDefenseCost { get; set; }
        public decimal DestinationDefenseCost { get; set; }
        public decimal EliteDefenseCost { get; set; }
    }
}
