using fastfood_production.Application.Shared.BaseResponse;
using MediatR;

namespace fastfood_production.Application.UseCases.GetAllProduction;

public sealed record class GetAllProductionRequest : IRequest<Result<GetAllProductionResponse>>;