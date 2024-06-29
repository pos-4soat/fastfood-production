using AutoMapper;
using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Domain.Contracts.RabbitMq;
using fastfood_production.Domain.Contracts.Repository;
using fastfood_production.Domain.Entity;
using fastfood_production.Domain.Enum;
using MediatR;

namespace fastfood_production.Application.UseCases.UpdateProduction;

public class UpdateProductionHandler(IMapper mapper,
        IConsumerService consumerService,
        IProductionRepository repository) : IRequestHandler<UpdateProductionRequest, Result<UpdateProductionResponse>>
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IConsumerService _consumerService = consumerService ?? throw new ArgumentNullException(nameof(consumerService));
    private readonly IProductionRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<Result<UpdateProductionResponse>> Handle(UpdateProductionRequest request, CancellationToken cancellationToken)
    {
        ProductionEntity result = await _repository.GetProductionAsync(request.OrderId, cancellationToken);

        if (result == null)
            return Result<UpdateProductionResponse>.Failure("PBE004");

        if (!result.ValidStatus(request.Status))
            return Result<UpdateProductionResponse>.Failure("PBE005");

        if (request.Status.Equals(ProductionStatus.InProgress))
            _consumerService.PublishOrder(result.OrderId, 3);

        if (request.Status.Equals(ProductionStatus.Finished))
            _consumerService.PublishOrder(result.OrderId, 4);

        result.Status = request.Status;

        await _repository.EditProductionAsync(result, cancellationToken);

        return Result<UpdateProductionResponse>.Success(_mapper.Map<UpdateProductionResponse>(result));
    }
}
