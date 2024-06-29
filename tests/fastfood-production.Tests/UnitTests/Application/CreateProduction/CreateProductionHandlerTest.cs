using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Application.UseCases.CreateProduction;
using fastfood_production.Domain.Entity;
using Moq;
using System.Net;

namespace fastfood_production.Tests.UnitTests.Application.CreateProduction;

public class CreateProductionHandlerTest : TestFixture
{
    [Test, Description("Should return production created successfully")]
    public async Task ShouldCreateProductionAsync()
    {
        CreateProductionRequest request = _modelFakerFactory.GenerateRequest<CreateProductionRequest>();

        _repositoryMock.SetupGetProductionAsync(null);

        CreateProductionHandler service = new(_mapper, _consumerMock.Object, _repositoryMock.Object);

        Result<CreateProductionResponse> result = await service.Handle(request, default);

        AssertExtensions.ResultIsSuccess(result, HttpStatusCode.Created);

        CreateProductionResponse response = result.Value;
        Assert.That(response.OrderId, Is.EqualTo(request.OrderId));

        _repositoryMock.VerifyGetProductionAsync(request.OrderId, Times.Once());
        _repositoryMock.VerifyAddProductionAsync(Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
        _consumerMock.VerifyPublishOrder();
        _consumerMock.VerifyNoOtherCalls();
    }

    [Test, Description("Should return production created previously")]
    public async Task ShouldReturnProductionCreatedPreviously()
    {
        CreateProductionRequest request = _modelFakerFactory.GenerateRequest<CreateProductionRequest>();
        ProductionEntity entity = _modelFakerFactory.GenerateProductionEntity();

        _repositoryMock.SetupGetProductionAsync(entity);

        CreateProductionHandler service = new(_mapper, _consumerMock.Object, _repositoryMock.Object);

        Result<CreateProductionResponse> result = await service.Handle(request, default);

        AssertExtensions.ResultIsFailure(result, "PBE002", HttpStatusCode.BadRequest);

        _repositoryMock.VerifyGetProductionAsync(request.OrderId, Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
        _consumerMock.VerifyNoOtherCalls();
    }
}