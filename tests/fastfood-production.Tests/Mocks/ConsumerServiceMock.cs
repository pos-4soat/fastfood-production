using fastfood_production.Domain.Contracts.RabbitMq;
using fastfood_production.Tests.UnitTests;
using Moq;

namespace fastfood_production.Tests.Mocks;

public class ConsumerServiceMock : BaseCustomMock<IConsumerService>
{
    public ConsumerServiceMock(TestFixture testFixture) : base(testFixture)
    {
        SetupPublishOrder();
    }

    public void SetupPublishOrder()
        => Setup(x => x.PublishOrder(It.IsAny<int>(), It.IsAny<int>()));

    public void VerifyPublishOrder(Times? times = null)
        => Verify(x => x.PublishOrder(It.IsAny<int>(), It.IsAny<int>()), times ?? Times.Once());
}
