using fastfood_production.Infra.Http;
using fastfood_production.Infra.Http.Dto;

namespace fastfood_production.Tests.UnitTests.Infrastructure.Http
{
    public class OrderHttpClientTests : TestFixture
    {
        [Test]
        public async Task ShouldFailReadFromJsonAsyncOnUpdateOrderStatus()
        {
            var client = new OrderHttpClient("https://webhook.d3fkon1.com/e5c1145f-4673-4f68-b29d-19fc8ef61f2f/");
            OrderRequest request = new(1 , 2);

            var result = await client.UpdateOrderStatus(request.Id, CancellationToken.None, request.Status);

            Assert.That(result, Is.True);
        }
    }
}
