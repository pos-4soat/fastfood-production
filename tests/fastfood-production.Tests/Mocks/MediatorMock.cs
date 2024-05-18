using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Domain.Enum;
using fastfood_production.Tests.UnitTests;
using MediatR;
using Moq;

namespace fastfood_production.Tests.Mocks;

public class MediatorMock<TRequest, TResponse>(TestFixture testFixture) : BaseCustomMock<IMediator>(testFixture) where TRequest : notnull
{
    public void SetupSendAsync(TResponse response, StatusResponse statusResponse)
        => Setup(x => x.Send(It.IsAny<TRequest>(), default))
            .ReturnsAsync(Result<TResponse>.Success(response, statusResponse));
}

