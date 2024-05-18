using fastfood_production.Domain.Enum;

namespace fastfood_production.Application.UseCases.GetProductionByStatus;

public sealed record GetProductionByStatusResponse
{
    public IEnumerable<GetProductionByStatus> Productions { get; set; }

    public GetProductionByStatusResponse(IEnumerable<GetProductionByStatus> productions)
    {
        Productions = productions;
    }
}

public sealed record GetProductionByStatus
{
    public int Id { get; set; }
    public ProductionStatus Status { get; set; }
    public DateTime ReceivedDate { get; set; }
    public DateTime LastUpdate { get; set; }
    public IEnumerable<GetProductionByStatusResponseData> Items { get; set; }
}

public sealed record GetProductionByStatusResponseData
{
    public string Name { get; set; }
    public int Quantity { get; set; }
}
