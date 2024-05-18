using fastfood_production.Domain.Enum;
using Newtonsoft.Json;

namespace fastfood_production.Application.Shared.BaseResponse;

public class ErrorResponse<TBody> : BaseResponse where TBody : class
{
    [JsonProperty(Order = 3)]
    public TBody? Error { get; set; }

    public ErrorResponse(TBody body) : base(StatusResponse.ERROR) => Error = body;
}