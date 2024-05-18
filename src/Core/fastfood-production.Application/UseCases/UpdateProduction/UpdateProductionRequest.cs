using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Domain.Enum;
using MediatR;

namespace fastfood_production.Application.UseCases.UpdateProduction;

public sealed record class UpdateProductionRequest(int OrderId, ProductionStatus Status) : IRequest<Result<UpdateProductionResponse>>;
