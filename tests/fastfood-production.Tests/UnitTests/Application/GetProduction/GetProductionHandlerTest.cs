using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Application.UseCases.GetAllProduction;
using fastfood_production.Application.UseCases.GetProduction;
using fastfood_production.Domain.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace fastfood_production.Tests.UnitTests.Application.GetProduction;

public class GetProductionHandlerTest : TestFixture
{
    [Test, Description("Should return production successfully")]
    public async Task ShouldReturnProductionSuccessfully()
    {
        GetProductionRequest request = _modelFakerFactory.GenerateRequest<GetProductionRequest>();

        ProductionEntity entity = _modelFakerFactory.GenerateProductionEntity();

        _repositoryMock.SetupGetProductionAsync(entity);

        GetProductionHandler service = new(_mapper, _repositoryMock.Object);

        Result<GetProductionResponse> result = await service.Handle(request, default);

        AssertExtensions.ResultIsSuccess(result);

        _repositoryMock.VerifyGetProductionAsync(request.OrderId, Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
    }

    [Test, Description("Should return production not found")]
    public async Task ShouldReturnProductionNotFound()
    {
        GetProductionRequest request = _modelFakerFactory.GenerateRequest<GetProductionRequest>();

        _repositoryMock.SetupGetProductionAsync(null);

        GetProductionHandler service = new(_mapper, _repositoryMock.Object);

        Result<GetProductionResponse> result = await service.Handle(request, default);

        AssertExtensions.ResultIsFailure(result, "PBE004", HttpStatusCode.BadRequest);

        _repositoryMock.VerifyGetProductionAsync(request.OrderId, Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
    }
}
