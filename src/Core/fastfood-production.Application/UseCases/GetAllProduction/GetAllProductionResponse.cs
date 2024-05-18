using fastfood_production.Domain.Enum;

namespace fastfood_production.Application.UseCases.GetAllProduction;

public sealed record GetAllProductionResponse
{
    public IEnumerable<GetAllProduction> Productions { get; set; }

    public GetAllProductionResponse(IEnumerable<GetAllProduction> productions)
    {
        Productions = productions;
    }
}

public sealed record GetAllProduction
{
    public int Id { get; set; }
    public ProductionStatus Status { get; set; }
    public DateTime ReceivedDate { get; set; }
    public DateTime LastUpdate { get; set; }
    public IEnumerable<GetAllProductionResponseData> Items { get; set; }
}

public sealed record GetAllProductionResponseData
{
    public string Name { get; set; }
    public int Quantity { get; set; }
}
