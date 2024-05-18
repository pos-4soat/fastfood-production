using fastfood_production.Domain.Enum;

namespace fastfood_production.Application.UseCases.UpdateProduction;

public sealed record UpdateProductionResponse
{
    public int OrderId { get; set; }
    public ProductionStatus Status { get; set; }
}
