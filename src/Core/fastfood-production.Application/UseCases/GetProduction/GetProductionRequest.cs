using fastfood_production.Application.Shared.BaseResponse;
using MediatR;

namespace fastfood_production.Application.UseCases.GetProduction;

public sealed record class GetProductionRequest(int OrderId) : IRequest<Result<GetProductionResponse>>;
