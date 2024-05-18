using AutoMapper;
using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Domain.Contracts.Http;
using fastfood_production.Domain.Contracts.Repository;
using fastfood_production.Domain.Entity;
using fastfood_production.Domain.Enum;
using MediatR;

namespace fastfood_production.Application.UseCases.CreateProduction;

public class CreateProductionHandler(IMapper mapper,
        IOrderHttpClient httpClient,
        IProductionRepository repository) : IRequestHandler<CreateProductionRequest, Result<CreateProductionResponse>>
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IOrderHttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private readonly IProductionRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<Result<CreateProductionResponse>> Handle(CreateProductionRequest request, CancellationToken cancellationToken)
    {
        ProductionEntity productionEntity = _mapper.Map<ProductionEntity>(request);

        ProductionEntity existingProduction = await _repository.GetProductionAsync(productionEntity.OrderId, cancellationToken);

        if (existingProduction != null)
            return Result<CreateProductionResponse>.Failure("PBE002");

        await _repository.AddProductionAsync(productionEntity, cancellationToken);

        bool updated = await _httpClient.UpdateOrderStatus(productionEntity.OrderId, cancellationToken);

        return !updated
            ? Result<CreateProductionResponse>.Failure("PBE003")
            : Result<CreateProductionResponse>.Success(_mapper.Map<CreateProductionResponse>(request), StatusResponse.CREATED);
    }
}