using fastfood_production.Application.Shared.BaseResponse;
using MediatR;

namespace fastfood_production.Application.UseCases.CreateProduction;

public sealed record class CreateProductionRequest : IRequest<Result<CreateProductionResponse>>
{
    public int OrderId { get; set; }
    public IList<Items> Items { get; set; }
}

public sealed record Items
{
    public string Name { get; set; }
    public int Quantity { get; set; }
}
