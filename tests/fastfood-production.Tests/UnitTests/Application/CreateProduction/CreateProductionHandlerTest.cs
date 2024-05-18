using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Application.UseCases.CreateProduction;
using fastfood_production.Domain.Entity;
using fastfood_production.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace fastfood_production.Tests.UnitTests.Application.CreatePayment;

public class CreateProductionHandlerTest : TestFixture
{
    [Test, Description("Should return production created successfully")]
    public async Task ShouldCreateProductionAsync()
    {
        CreateProductionRequest request = _modelFakerFactory.GenerateRequest<CreateProductionRequest>();

        _repositoryMock.SetupGetProductionAsync(null);
        _orderHttpClientMock.SetupUpdateOrderStatus(true);

        CreateProductionHandler service = new(_mapper, _orderHttpClientMock.Object, _repositoryMock.Object);

        Result<CreateProductionResponse> result = await service.Handle(request, default);

        AssertExtensions.ResultIsSuccess(result, HttpStatusCode.Created);

        CreateProductionResponse response = result.Value;
        Assert.That(response.OrderId, Is.EqualTo(request.OrderId));

        _repositoryMock.VerifyGetProductionAsync(request.OrderId, Times.Once());
        _repositoryMock.VerifyAddProductionAsync(Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
        _orderHttpClientMock.VerifyUpdateOrderStatus(request.OrderId, Times.Once());
        _orderHttpClientMock.VerifyNoOtherCalls();
    }

    [Test, Description("Should return production created previously")]
    public async Task ShouldReturnProductionCreatedPreviously()
    {
        CreateProductionRequest request = _modelFakerFactory.GenerateRequest<CreateProductionRequest>();
        ProductionEntity entity = _modelFakerFactory.GenerateProductionEntity();

        _repositoryMock.SetupGetProductionAsync(entity);

        CreateProductionHandler service = new(_mapper, _orderHttpClientMock.Object, _repositoryMock.Object);

        Result<CreateProductionResponse> result = await service.Handle(request, default);

        AssertExtensions.ResultIsFailure(result, "PBE002", HttpStatusCode.BadRequest);

        _repositoryMock.VerifyGetProductionAsync(request.OrderId, Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
        _orderHttpClientMock.VerifyNoOtherCalls();
    }

    [Test, Description("Should return fail order update")]
    public async Task ShouldReturnFailedOrderUpdate()
    {
        CreateProductionRequest request = _modelFakerFactory.GenerateRequest<CreateProductionRequest>();

        _repositoryMock.SetupGetProductionAsync(null);
        _orderHttpClientMock.SetupUpdateOrderStatus(false);

        CreateProductionHandler service = new(_mapper, _orderHttpClientMock.Object, _repositoryMock.Object);

        Result<CreateProductionResponse> result = await service.Handle(request, default);

        AssertExtensions.ResultIsFailure(result, "PBE003", HttpStatusCode.BadRequest);

        _repositoryMock.VerifyGetProductionAsync(request.OrderId, Times.Once());
        _repositoryMock.VerifyAddProductionAsync(Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
        _orderHttpClientMock.VerifyUpdateOrderStatus(request.OrderId, Times.Once());
        _orderHttpClientMock.VerifyNoOtherCalls();
    }
}