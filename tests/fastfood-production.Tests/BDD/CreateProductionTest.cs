using fastfood_production.API.Controllers;
using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Application.UseCases.UpdateProduction;
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
    private UpdateProductionRequest _request;
    private IActionResult _result;

    [Test, Description("")]
    public async Task CreateANewPayment()
    {
        GivenIHaveAValidCreateProductionRequest();
        GivenTheRepositoryReturnsASuccessfulResult();
        await WhenIRequestAProductionUpdate();
        ThenTheResultShouldBeASuccessResult();
    }

    [Given(@"I have a valid update production request")]
    public void GivenIHaveAValidCreateProductionRequest()
    {
        _request = _modelFakerFactory.GenerateRequest<UpdateProductionRequest>();
    }

    [Given(@"the repository returns a successful result")]
    public void GivenTheRepositoryReturnsASuccessfulResult()
    {
        _mediatorMock = new Mock<IMediator>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateProductionRequest>(), default))
            .ReturnsAsync(Result<UpdateProductionResponse>.Success(new UpdateProductionResponse()
            {
                OrderId = _request.OrderId,
                Status = ProductionStatus.Delivered
            }, StatusResponse.SUCCESS));
    }

    [When(@"I request a production update")]
    public async Task WhenIRequestAProductionUpdate()
    {
        ProductionController controller = new ProductionController(_mediatorMock.Object);

        _result = await controller.UpdateProduction(_request, default);
    }

    [Then(@"the result should be a CreatedResult")]
    public void ThenTheResultShouldBeASuccessResult()
    {
        ObjectResult? objectResult = _result as ObjectResult;
        Assert.That(objectResult, Is.Not.Null);
        Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));

        Response<object>? response = objectResult.Value as Response<object>;
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Status, Is.EqualTo(nameof(StatusResponse.SUCCESS)));

        UpdateProductionResponse? body = response.Body as UpdateProductionResponse;
        Assert.That(body.OrderId, Is.EqualTo(_request.OrderId));
        Assert.That(body.Status, Is.EqualTo(ProductionStatus.Delivered));
    }
}
