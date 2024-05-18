using AutoMapper;
using fastfood_production.Domain.Entity;

namespace fastfood_production.Application.UseCases.CreateProduction;

internal class CreateProductionMapper : Profile
{
    public CreateProductionMapper()
    {
        CreateMap<CreateProductionRequest, ProductionEntity>()
            .ForMember(dest => dest.ProductionItems, opt => opt.MapFrom(src => MapItems(src.Items)));
        CreateMap<ProductionEntity, CreateProductionResponse>();
        CreateMap<CreateProductionRequest, CreateProductionResponse>();
    }

    private IEnumerable<ProductionItemEntity> MapItems(IEnumerable<Items> orderedItems)
    {
        return orderedItems.Select(item => new ProductionItemEntity
        {
            Name = item.Name,
            Quantity = item.Quantity,
        });
    }
}