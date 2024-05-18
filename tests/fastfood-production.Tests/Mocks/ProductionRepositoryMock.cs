using fastfood_production.Domain.Contracts.Repository;
using fastfood_production.Domain.Entity;
using fastfood_production.Domain.Enum;
using fastfood_production.Tests.UnitTests;
using Moq;

namespace fastfood_production.Tests.Mocks;

public class ProductionRepositoryMock : BaseCustomMock<IProductionRepository>
{
    public ProductionRepositoryMock(TestFixture testFixture) : base(testFixture)
    {
        SetupAddProductionAsync();
        SetupEditProductionAsync();
    }

    public void SetupAddProductionAsync()
        => Setup(x => x.AddProductionAsync(It.IsAny<ProductionEntity>(), default))
            .Returns(Task.CompletedTask);

    public void SetupEditProductionAsync()
        => Setup(x => x.EditProductionAsync(It.IsAny<ProductionEntity>(), default))
            .Returns(Task.CompletedTask);

    public void SetupGetAllAsync(IEnumerable<ProductionEntity> expectedReturn)
        => Setup(x => x.GetAllAsync(default))
            .ReturnsAsync(expectedReturn);

    public void SetupGetProductionAsync(ProductionEntity expectedReturn)
        => Setup(x => x.GetProductionAsync(It.IsAny<int>(), default))
            .ReturnsAsync(expectedReturn);

    public void SetupGetProductionsByStatus(IEnumerable<ProductionEntity> expectedReturn)
        => Setup(x => x.GetProductionsByStatus(It.IsAny<ProductionStatus>(), default))
            .ReturnsAsync(expectedReturn);

    public void VerifyAddProductionAsync(Times? times = null)
        => Verify(x => x.AddProductionAsync(It.IsAny<ProductionEntity>(), default), times ?? Times.Once());

    public void VerifyEditProductionAsync(Times? times = null)
        => Verify(x => x.EditProductionAsync(It.IsAny<ProductionEntity>(), default), times ?? Times.Once());

    public void VerifyGetAllAsync(Times? times = null)
        => Verify(x => x.GetAllAsync(default), times ?? Times.Once());

    public void VerifyGetProductionAsync(int orderId, Times? times = null)
        => Verify(x => x.GetProductionAsync(orderId, default), times ?? Times.Once());

    public void VerifyGetProductionsByStatus(Times? times = null)
        => Verify(x => x.GetProductionsByStatus(It.IsAny<ProductionStatus>(), default), times ?? Times.Once());
}
