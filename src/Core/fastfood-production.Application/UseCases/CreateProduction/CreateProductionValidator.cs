using FluentValidation;

namespace fastfood_production.Application.UseCases.CreateProduction;

public class CreateProductionValidator : AbstractValidator<CreateProductionRequest>
{
    public CreateProductionValidator()
    {
        RuleFor(dto => dto.Items)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("PBE007")
            .ForEach(item => item.SetValidator(new CreateProductionItemValidator()));

        RuleFor(dto => dto.OrderId)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithMessage("PBE006");
    }
}

public class CreateProductionItemValidator : AbstractValidator<Items>
{
    public CreateProductionItemValidator()
    {
        RuleFor(dto => dto.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("PBE008");

        RuleFor(dto => dto.Quantity)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithMessage("PBE009");
    }
}