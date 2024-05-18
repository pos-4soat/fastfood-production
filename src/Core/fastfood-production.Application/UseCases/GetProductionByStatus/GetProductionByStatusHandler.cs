using AutoMapper;
using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Domain.Contracts.Repository;
using fastfood_production.Domain.Entity;
using MediatR;

namespace fastfood_production.Application.UseCases.GetProductionByStatus;

public class GetProductionByStatusHandler(IMapper mapper,
        IProductionRepository repository) : IRequestHandler<GetProductionByStatusRequest, Result<GetProductionByStatusResponse>>
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IProductionRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<Result<GetProductionByStatusResponse>> Handle(GetProductionByStatusRequest request, CancellationToken cancellationToken)
    {
        List<ProductionEntity> result = (await _repository.GetProductionsByStatus(request.status, cancellationToken)).ToList();

        List<GetProductionByStatus> productionList = [.. result.Select(entity =>
        {
            GetProductionByStatus productions = _mapper.Map<GetProductionByStatus>(entity);

            productions.Items = entity.ProductionItems.Select(item =>
            {
                return new GetProductionByStatusResponseData
                {
                    Name = item.Name,
                    Quantity = item.Quantity,
                };
            });

            return productions;
        }).OrderBy(x => x.ReceivedDate)];

        GetProductionByStatusResponse response = new GetProductionByStatusResponse(productionList);

        return Result<GetProductionByStatusResponse>.Success(response);
    }
}