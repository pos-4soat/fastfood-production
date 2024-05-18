using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Application.UseCases.GetAllProduction;
using fastfood_production.Application.UseCases.GetProduction;
using fastfood_production.Application.UseCases.GetProductionByStatus;
using fastfood_production.Domain.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace fastfood_production.Tests.UnitTests.Application.GetProductionByStatus;

public class GetProductionByStatusHandlerTest : TestFixture
{
    [Test, Description("Should return productions by status successfully")]
    public async Task ShouldReturnProductionsByStatusSuccessfully()
    {
        GetProductionByStatusRequest request = _modelFakerFactory.GenerateRequest<GetProductionByStatusRequest>();

        ProductionEntity entity = _modelFakerFactory.GenerateProductionEntity();
        List<ProductionEntity> entityList = [entity];

        _repositoryMock.SetupGetProductionsByStatus(entityList);

        GetProductionByStatusHandler service = new(_mapper, _repositoryMock.Object);

        Result<GetProductionByStatusResponse> result = await service.Handle(request, default);

        AssertExtensions.ResultIsSuccess(result);

        _repositoryMock.VerifyGetProductionsByStatus(Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
    }
}
