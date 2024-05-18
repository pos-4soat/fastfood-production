using AutoMapper;
using fastfood_production.Domain.Entity;

namespace fastfood_production.Application.UseCases.UpdateProduction;

internal class UpdateProductionMapper : Profile
{
    public UpdateProductionMapper()
    {
        CreateMap<ProductionEntity, UpdateProductionResponse>();
    }
}
