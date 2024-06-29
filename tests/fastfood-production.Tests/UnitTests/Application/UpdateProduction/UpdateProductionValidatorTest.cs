using fastfood_production.Application.UseCases.UpdateProduction;

namespace fastfood_production.Tests.UnitTests.Application.UpdateProduction;

public class UpdateProductionValidatorTest : TestFixture
{
    private UpdateProductionValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new UpdateProductionValidator();
    }

    [Test]
    public void ShouldValidateOrderIdRequirement()
    {
        UpdateProductionRequest request = new(-1, Domain.Enum.ProductionStatus.InProgress);

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE006");
    }

    [Test]
    public void ShouldValidateStatusRequirement()
    {
        UpdateProductionRequest request = new(1, (Domain.Enum.ProductionStatus)99);

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE010");
    }
}
