using fastfood_production.Application.UseCases.CreateProduction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fastfood_production.Tests.UnitTests.Application.CreateProduction;

public class CreateProductionValidatorTest : TestFixture
{
    private CreateProductionValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new CreateProductionValidator();
    }

    [Test]
    public void ShouldValidateItemsRequirement()
    {
        CreateProductionRequest request = _modelFakerFactory.GenerateRequest<CreateProductionRequest>();

        request.Items.Clear();

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE007");
    }

    [Test]
    public void ShouldValidateOrderIdRequirement()
    {
        CreateProductionRequest request = _modelFakerFactory.GenerateRequest<CreateProductionRequest>();

        request.OrderId = -1;

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE006");
    }

    [Test]
    public void ShouldValidateItemNameRequirement()
    {
        CreateProductionRequest request = _modelFakerFactory.GenerateRequest<CreateProductionRequest>();

        request.Items.Add(new Items());

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE008");
    }

    [Test]
    public void ShouldValidateItemQuantityRequirement()
    {
        CreateProductionRequest request = _modelFakerFactory.GenerateRequest<CreateProductionRequest>();

        request.Items.Add(new() { Name = "test", Quantity = -1 });

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE009");
    }
}
