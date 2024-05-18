using fastfood_production.Domain.Enum;

namespace fastfood_production.Application.UseCases.GetProduction;

public sealed record GetProductionResponse
{
    public int Id { get; set; }
    public ProductionStatus Status { get; set; }
    public DateTime ReceivedDate { get; set; }
    public DateTime LastUpdate { get; set; }
    public IEnumerable<GetProductionResponseData> Items { get; set; }
}

public sealed record GetProductionResponseData
{
    public string Name { get; set; }
    public int Quantity { get; set; }
}
