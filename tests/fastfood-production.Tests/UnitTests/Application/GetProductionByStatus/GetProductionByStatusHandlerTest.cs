using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Application.UseCases.GetProductionByStatus;
using fastfood_production.Domain.Entity;
using Moq;

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
