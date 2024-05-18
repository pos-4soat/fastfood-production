using fastfood_production.Application.UseCases.CreateProduction;
using fastfood_production.Application.UseCases.UpdateProduction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
