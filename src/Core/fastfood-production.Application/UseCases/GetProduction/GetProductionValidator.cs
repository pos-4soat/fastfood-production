using FluentValidation;

namespace fastfood_production.Application.UseCases.GetProduction;

public class GetProductionValidator : AbstractValidator<GetProductionRequest>
{
    public GetProductionValidator()
    {
    }
}