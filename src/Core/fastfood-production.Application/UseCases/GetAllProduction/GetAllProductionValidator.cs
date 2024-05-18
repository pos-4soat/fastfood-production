using FluentValidation;

namespace fastfood_production.Application.UseCases.GetAllProduction;

public class GetAllProductionValidator : AbstractValidator<GetAllProductionRequest>
{
    public GetAllProductionValidator() { }
}