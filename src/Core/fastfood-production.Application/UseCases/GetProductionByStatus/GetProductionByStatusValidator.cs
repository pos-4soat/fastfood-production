using FluentValidation;

namespace fastfood_production.Application.UseCases.GetProductionByStatus;

public class GetProductionByStatusValidator : AbstractValidator<GetProductionByStatusRequest>
{
    public GetProductionByStatusValidator()
    {
    }
}