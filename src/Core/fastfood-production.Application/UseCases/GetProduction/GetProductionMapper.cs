﻿using AutoMapper;
using fastfood_production.Domain.Entity;

namespace fastfood_production.Application.UseCases.GetProduction;

internal class GetProductionMapper : Profile
{
    public GetProductionMapper()
    {
        CreateMap<ProductionEntity, GetProductionResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ReceivedDate, opt => opt.MapFrom(src => src.CreationDate))
            .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => src.UpdateDate));
    }
}
