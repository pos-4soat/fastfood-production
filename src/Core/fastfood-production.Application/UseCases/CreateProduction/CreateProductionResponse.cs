namespace fastfood_production.Application.UseCases.CreateProduction;

public sealed record CreateProductionResponse
{
    public int OrderId { get; set; }
    public IList<Items> Items { get; set; }
}