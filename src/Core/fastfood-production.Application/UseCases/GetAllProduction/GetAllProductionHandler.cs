using AutoMapper;
using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Domain.Contracts.Repository;
using fastfood_production.Domain.Entity;
using MediatR;

namespace fastfood_production.Application.UseCases.GetAllProduction;

public class GetAllProductionHandler(IMapper mapper,
        IProductionRepository repository) : IRequestHandler<GetAllProductionRequest, Result<GetAllProductionResponse>>
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IProductionRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<Result<GetAllProductionResponse>> Handle(GetAllProductionRequest request, CancellationToken cancellationToken)
    {
        List<ProductionEntity> result = (await _repository.GetAllAsync(cancellationToken)).ToList();

        List<GetAllProduction> productionList = [.. result.Select(entity =>
        {
            GetAllProduction productions = _mapper.Map<GetAllProduction>(entity);

            productions.Items = entity.ProductionItems.Select(item =>
            {
                return new GetAllProductionResponseData
                {
                    Name = item.Name,
                    Quantity = item.Quantity,
                };
            });

            return productions;
        }).OrderBy(x => x.Status).OrderBy(x => x.ReceivedDate)];

        GetAllProductionResponse response = new GetAllProductionResponse(productionList);

        return Result<GetAllProductionResponse>.Success(response);
    }
}