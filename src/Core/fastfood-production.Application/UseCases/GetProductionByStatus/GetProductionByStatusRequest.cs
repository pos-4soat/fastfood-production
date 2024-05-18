using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Domain.Enum;
using MediatR;

namespace fastfood_production.Application.UseCases.GetProductionByStatus;

public sealed record class GetProductionByStatusRequest(ProductionStatus status) : IRequest<Result<GetProductionByStatusResponse>>;

