using AutoMapper;
using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Domain.Contracts.Repository;
using fastfood_production.Domain.Entity;
using MediatR;

namespace fastfood_production.Application.UseCases.GetProduction;

public class GetProductionHandler(IMapper mapper,
        IProductionRepository repository) : IRequestHandler<GetProductionRequest, Result<GetProductionResponse>>
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IProductionRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<Result<GetProductionResponse>> Handle(GetProductionRequest request, CancellationToken cancellationToken)
    {
        ProductionEntity result = await _repository.GetProductionAsync(request.OrderId, cancellationToken);

        if (result == null)
            return Result<GetProductionResponse>.Failure("PBE004");

        GetProductionResponse productions = _mapper.Map<GetProductionResponse>(result);

        productions.Items = result.ProductionItems.Select(item =>
        {
            return new GetProductionResponseData
            {
                Name = item.Name,
                Quantity = item.Quantity,
            };
        });

        return Result<GetProductionResponse>.Success(productions);
    }
}