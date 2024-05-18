using fastfood_production.API.Controllers;
using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Application.UseCases.CreateProduction;
using fastfood_production.Domain.Enum;
using fastfood_production.Tests.UnitTests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using TechTalk.SpecFlow;

namespace fastfood_production.Tests.BDD;

[TestFixture]
public class CreateProductionTest : TestFixture
{
    private Mock<IMediator> _mediatorMock;
    private CreateProductionRequest _request;
    private IActionResult _result;

    [Test, Description("")]
    public async Task CreateANewPayment()
    {
        GivenIHaveAValidCreateProductionRequest();
        GivenTheRepositoryReturnsASuccessfulResult();
        await WhenIRequestAProductionCreation();
        ThenTheResultShouldBeACreatedResult();
    }

    [Given(@"I have a valid create payment request")]
    public void GivenIHaveAValidCreateProductionRequest()
    {
        _request = new CreateProductionRequest()
        {
            Items =
            [
                new Items
                {
                    Name = "Test",
                    Quantity = 1
                }
            ],
            OrderId = 1
        };
    }

    [Given(@"the repository returns a successful result")]
    public void GivenTheRepositoryReturnsASuccessfulResult()
    {
        _mediatorMock = new Mock<IMediator>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<CreateProductionRequest>(), default))
            .ReturnsAsync(Result<CreateProductionResponse>.Success(new CreateProductionResponse()
            {
                OrderId = 1,
                Items = [
                    new Items
                    {
                    Name = "Test",
                    Quantity = 1
                    }
                ]
            }, StatusResponse.CREATED));
    }

    [When(@"I request a payment creation")]
    public async Task WhenIRequestAProductionCreation()
    {
        ProductionController controller = new ProductionController(_mediatorMock.Object);

        _result = await controller.CreateProduction(_request, default);
    }

    [Then(@"the result should be a CreatedResult")]
    public void ThenTheResultShouldBeACreatedResult()
    {
        ObjectResult? objectResult = _result as ObjectResult;
        Assert.That(objectResult, Is.Not.Null);
        Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));

        Response<object>? response = objectResult.Value as Response<object>;
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Status, Is.EqualTo(nameof(StatusResponse.CREATED)));

        CreateProductionResponse? body = response.Body as CreateProductionResponse;
        Assert.That(body.OrderId, Is.EqualTo(_request.OrderId));
        Assert.That(body.Items.Count, Is.EqualTo(_request.Items.Count));
    }
}
