using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Application.UseCases.GetAllProduction;
using fastfood_production.Domain.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fastfood_production.Tests.UnitTests.Application.GetAllProduction;

public class GetAllProductionHandlerTest : TestFixture
{
    [Test, Description("Should return all productions successfully")]
    public async Task ShouldReturnAllProductionsSuccessfully()
    {
        GetAllProductionRequest request = _modelFakerFactory.GenerateRequest<GetAllProductionRequest>();

        ProductionEntity entity = _modelFakerFactory.GenerateProductionEntity();
        List<ProductionEntity> entityList = [entity];

        _repositoryMock.SetupGetAllAsync(entityList);

        GetAllProductionHandler service = new(_mapper, _repositoryMock.Object);

        Result<GetAllProductionResponse> result = await service.Handle(request, default);

        AssertExtensions.ResultIsSuccess(result);

        _repositoryMock.VerifyGetAllAsync(Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
    }
}
