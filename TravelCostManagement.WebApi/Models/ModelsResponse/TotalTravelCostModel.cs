namespace TravelCostManagement.WebApi.Models.ModelsResponse
{
    public class TotalTravelCostModel
    {
        public decimal TotalCost { get; set; }

        public decimal PricesForLunarDay { get; set; }

        public TaxesModel TaxesCost { get; set; }
    }

    public class TaxesModel
    {
        public decimal OriginDefenseCost  { get; set; }
        public decimal DestinationDefenseCost { get; set; }
        public decimal EliteDefenseCost { get; set; }
    }
}
