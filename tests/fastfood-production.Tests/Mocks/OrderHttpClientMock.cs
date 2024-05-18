using fastfood_production.Domain.Contracts.Http;
using fastfood_production.Tests.UnitTests;
using Moq;

namespace fastfood_production.Tests.Mocks;

public class OrderHttpClientMock : BaseCustomMock<IOrderHttpClient>
{
    public OrderHttpClientMock(TestFixture testFixture) : base(testFixture)
    {
    }

    public void SetupUpdateOrderStatus(bool expectedReturn)
        => Setup(x => x.UpdateOrderStatus(It.IsAny<int>(), default, It.IsAny<int>()))
            .ReturnsAsync(expectedReturn);

    public void VerifyUpdateOrderStatus(int orderId, Times? times = null)
        => Verify(x => x.UpdateOrderStatus(It.IsAny<int>(), default, It.IsAny<int>()), times ?? Times.Once());
}
