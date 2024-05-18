using AutoMapper;
using fastfood_production.Domain.Entity;

namespace fastfood_production.Application.UseCases.GetAllProduction;

internal class GetAllProductionMapper : Profile
{
    public GetAllProductionMapper()
    {
        CreateMap<ProductionEntity, GetAllProduction>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ReceivedDate, opt => opt.MapFrom(src => src.CreationDate))
            .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => src.UpdateDate))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => MapItems(src.ProductionItems)));
    }

    private IEnumerable<GetAllProductionResponseData> MapItems(IEnumerable<ProductionItemEntity> items)
    {
        return items.Select(item => new GetAllProductionResponseData
        {
            Name = item.Name,
            Quantity = item.Quantity,
        });
    }
}
