using fastfood_production.API.Controllers;
using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Application.UseCases.GetAllProduction;
using fastfood_production.Application.UseCases.GetProduction;
using fastfood_production.Application.UseCases.GetProductionByStatus;
using fastfood_production.Application.UseCases.UpdateProduction;
using fastfood_production.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace fastfood_production.Tests.UnitTests.Controller;

public class ProductionControllerTest : TestFixture
{
    [Test, Description("")]
    public async Task ShouldUpdateProduction()
    {
        UpdateProductionRequest request = _modelFakerFactory.GenerateRequest<UpdateProductionRequest>();

        Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateProductionRequest>(), default))
            .ReturnsAsync(Result<UpdateProductionResponse>.Success(_modelFakerFactory.GenerateRequest<UpdateProductionResponse>()));

        ProductionController service = new(_mediatorMock.Object);

        IActionResult result = await service.UpdateProduction(request, default);

        AssertExtensions.AssertResponse<UpdateProductionRequest, UpdateProductionResponse>(result, HttpStatusCode.OK, nameof(StatusResponse.SUCCESS), null);
    }

    [Test, Description("")]
    public async Task ShouldGetProduction()
    {
        GetProductionRequest request = _modelFakerFactory.GenerateRequest<GetProductionRequest>();

        Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetProductionRequest>(), default))
            .ReturnsAsync(Result<GetProductionResponse>.Success(_modelFakerFactory.GenerateRequest<GetProductionResponse>()));

        ProductionController service = new(_mediatorMock.Object);

        IActionResult result = await service.GetProduction(request.OrderId, default);

        AssertExtensions.AssertResponse<GetProductionRequest, GetProductionResponse>(result, HttpStatusCode.OK, nameof(StatusResponse.SUCCESS), request);
    }

    [Test, Description("")]
    public async Task ShouldGetAllProductions()
    {
        GetAllProductionRequest request = _modelFakerFactory.GenerateRequest<GetAllProductionRequest>();

        Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetAllProductionRequest>(), default))
            .ReturnsAsync(Result<GetAllProductionResponse>.Success(_modelFakerFactory.GenerateRequest<GetAllProductionResponse>()));

        ProductionController service = new(_mediatorMock.Object);

        IActionResult result = await service.GetAllProductions(default);

        AssertExtensions.AssertResponse<GetAllProductionRequest, GetAllProductionResponse>(result, HttpStatusCode.OK, nameof(StatusResponse.SUCCESS), null);
    }

    [Test, Description("")]
    public async Task ShouldGetProductionsByStatus()
    {
        GetProductionByStatusRequest request = _modelFakerFactory.GenerateRequest<GetProductionByStatusRequest>();

        Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetProductionByStatusRequest>(), default))
            .ReturnsAsync(Result<GetProductionByStatusResponse>.Success(_modelFakerFactory.GenerateRequest<GetProductionByStatusResponse>()));

        ProductionController service = new(_mediatorMock.Object);

        IActionResult result = await service.GetProductionsByStatus(request.status, default);

        AssertExtensions.AssertResponse<GetProductionByStatusRequest, GetProductionByStatusResponse>(result, HttpStatusCode.OK, nameof(StatusResponse.SUCCESS), null);
    }

    [Test, Description("")]
    public async Task ShouldReturnPaymentNotFound()
    {
        UpdateProductionRequest request = _modelFakerFactory.GenerateRequest<UpdateProductionRequest>();

        Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateProductionRequest>(), default))
            .ReturnsAsync(Result<UpdateProductionResponse>.Failure("PBE004"));

        ProductionController service = new(_mediatorMock.Object);

        IActionResult result = await service.UpdateProduction(request, default);

        AssertExtensions.AssertErrorResponse(result, HttpStatusCode.BadRequest, nameof(StatusResponse.ERROR));
    }
}
