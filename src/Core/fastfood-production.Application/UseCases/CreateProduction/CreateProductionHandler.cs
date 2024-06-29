using AutoMapper;
using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Domain.Contracts.RabbitMq;
using fastfood_production.Domain.Contracts.Repository;
using fastfood_production.Domain.Entity;
using fastfood_production.Domain.Enum;
using MediatR;

namespace fastfood_production.Application.UseCases.CreateProduction;

public class CreateProductionHandler(IMapper mapper,
        IConsumerService consumerService,
        IProductionRepository repository) : IRequestHandler<CreateProductionRequest, Result<CreateProductionResponse>>
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IConsumerService _consumerService = consumerService ?? throw new ArgumentNullException(nameof(consumerService));
    private readonly IProductionRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<Result<CreateProductionResponse>> Handle(CreateProductionRequest request, CancellationToken cancellationToken)
    {
        ProductionEntity productionEntity = _mapper.Map<ProductionEntity>(request);

        ProductionEntity existingProduction = await _repository.GetProductionAsync(productionEntity.OrderId, cancellationToken);

        if (existingProduction != null)
            return Result<CreateProductionResponse>.Failure("PBE002");

        await _repository.AddProductionAsync(productionEntity, cancellationToken);

        _consumerService.PublishOrder(productionEntity.OrderId, 2);

        return Result<CreateProductionResponse>.Success(_mapper.Map<CreateProductionResponse>(request), StatusResponse.CREATED);
    }
}