using AutoMapper;
using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Domain.Contracts.Http;
using fastfood_production.Domain.Contracts.Repository;
using fastfood_production.Domain.Entity;
using fastfood_production.Domain.Enum;
using MediatR;

namespace fastfood_production.Application.UseCases.UpdateProduction;

public class UpdateProductionHandler(IMapper mapper,
        IOrderHttpClient httpClient,
        IProductionRepository repository) : IRequestHandler<UpdateProductionRequest, Result<UpdateProductionResponse>>
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IOrderHttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private readonly IProductionRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<Result<UpdateProductionResponse>> Handle(UpdateProductionRequest request, CancellationToken cancellationToken)
    {
        ProductionEntity result = await _repository.GetProductionAsync(request.OrderId, cancellationToken);

        if (result == null)
            return Result<UpdateProductionResponse>.Failure("PBE004");

        if (!result.ValidStatus(request.Status))
            return Result<UpdateProductionResponse>.Failure("PBE005");

        if (request.Status.Equals(ProductionStatus.InProgress))
        {
            bool updated = await _httpClient.UpdateOrderStatus(result.OrderId, cancellationToken, 3);

            if (!updated)
                return Result<UpdateProductionResponse>.Failure("PBE003");
        }

        if (request.Status.Equals(ProductionStatus.Finished))
        {
            bool updated = await _httpClient.UpdateOrderStatus(result.OrderId, cancellationToken, (int)ProductionStatus.Delivered);

            if (!updated)
                return Result<UpdateProductionResponse>.Failure("PBE003");
        }
        result.Status = request.Status;

        await _repository.EditProductionAsync(result, cancellationToken);

        return Result<UpdateProductionResponse>.Success(_mapper.Map<UpdateProductionResponse>(result));
    }
}
