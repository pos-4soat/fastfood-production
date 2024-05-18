using FluentValidation;

namespace fastfood_production.Application.UseCases.UpdateProduction;

public class UpdateProductionValidator : AbstractValidator<UpdateProductionRequest>
{
    public UpdateProductionValidator()
    {
        RuleFor(dto => dto.OrderId)
            .GreaterThan(0)
            .WithMessage("PBE006");

        RuleFor(dto => dto.Status)
            .IsInEnum()
            .WithMessage("PBE010");
    }
}
