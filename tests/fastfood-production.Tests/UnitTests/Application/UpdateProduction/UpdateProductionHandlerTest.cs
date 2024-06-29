using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Application.UseCases.UpdateProduction;
using fastfood_production.Domain.Entity;
using Moq;
using System.Net;

namespace fastfood_production.Tests.UnitTests.Application.UpdateProduction;

public class UpdateProductionHandlerTest : TestFixture
{
    [Test, Description("Should update production successfully")]
    public async Task ShouldUpdateProductionSuccessfully()
    {
        UpdateProductionRequest request = new(1, (Domain.Enum.ProductionStatus)2);

        ProductionEntity entity = _modelFakerFactory.GenerateProductionEntity();
        entity.Status = (Domain.Enum.ProductionStatus)1;

        _repositoryMock.SetupGetProductionAsync(entity);

        UpdateProductionHandler service = new(_mapper, _consumerMock.Object, _repositoryMock.Object);

        Result<UpdateProductionResponse> result = await service.Handle(request, default);

        AssertExtensions.ResultIsSuccess(result);

        _repositoryMock.VerifyGetProductionAsync(request.OrderId, Times.Once());
        _repositoryMock.VerifyEditProductionAsync(Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
        _consumerMock.VerifyPublishOrder();
        _consumerMock.VerifyNoOtherCalls();
    }

    [Test, Description("Should update production t0 finished successfully")]
    public async Task ShouldUpdateProductionFinishedSuccessfully()
    {
        UpdateProductionRequest request = new(1, (Domain.Enum.ProductionStatus)3);

        ProductionEntity entity = _modelFakerFactory.GenerateProductionEntity();
        entity.Status = (Domain.Enum.ProductionStatus)2;

        _repositoryMock.SetupGetProductionAsync(entity);

        UpdateProductionHandler service = new(_mapper, _consumerMock.Object, _repositoryMock.Object);

        Result<UpdateProductionResponse> result = await service.Handle(request, default);

        AssertExtensions.ResultIsSuccess(result);

        _repositoryMock.VerifyGetProductionAsync(request.OrderId, Times.Once());
        _repositoryMock.VerifyEditProductionAsync(Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
        _consumerMock.VerifyPublishOrder();
        _consumerMock.VerifyNoOtherCalls();
    }

    [Test, Description("Should return production not found")]
    public async Task ShouldReturnProdctionNotFound()
    {
        UpdateProductionRequest request = _modelFakerFactory.GenerateRequest<UpdateProductionRequest>();

        _repositoryMock.SetupGetProductionAsync(null);

        UpdateProductionHandler service = new(_mapper, _consumerMock.Object, _repositoryMock.Object);

        Result<UpdateProductionResponse> result = await service.Handle(request, default);

        AssertExtensions.ResultIsFailure(result, "PBE004", HttpStatusCode.BadRequest);

        _repositoryMock.VerifyGetProductionAsync(request.OrderId, Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
        _consumerMock.VerifyNoOtherCalls();
    }

    [Test, Description("Should return new status invalid")]
    public async Task ShouldReturnNewStatusInvalid()
    {
        UpdateProductionRequest request = new(1, (Domain.Enum.ProductionStatus)3);

        ProductionEntity entity = _modelFakerFactory.GenerateProductionEntity();
        entity.Status = (Domain.Enum.ProductionStatus)3;

        _repositoryMock.SetupGetProductionAsync(entity);

        UpdateProductionHandler service = new(_mapper, _consumerMock.Object, _repositoryMock.Object);

        Result<UpdateProductionResponse> result = await service.Handle(request, default);

        AssertExtensions.ResultIsFailure(result, "PBE005", HttpStatusCode.BadRequest);

        _repositoryMock.VerifyGetProductionAsync(request.OrderId, Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
        _consumerMock.VerifyNoOtherCalls();
    }
}
