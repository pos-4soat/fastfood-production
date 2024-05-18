using fastfood_production.API.Controllers.Base;
using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Application.UseCases.CreateProduction;
using fastfood_production.Application.UseCases.GetAllProduction;
using fastfood_production.Application.UseCases.GetProduction;
using fastfood_production.Application.UseCases.GetProductionByStatus;
using fastfood_production.Application.UseCases.UpdateProduction;
using fastfood_production.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace fastfood_production.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{ver:apiVersion}/[controller]")]
    public class ProductionController(IMediator _mediator) : BaseController
    {
        [HttpPost]
        [SwaggerOperation(Summary = "Create production order")]
        [SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<Result<CreateProductionResponse>>))]
        public async Task<IActionResult> CreateProduction([FromBody] CreateProductionRequest createRequest, CancellationToken cancellationToken)
        {
            Result<CreateProductionResponse> result = await _mediator.Send(createRequest, cancellationToken);
            return await GetResponseFromResult(result);
        }

        [HttpPatch]
        [SwaggerOperation(Summary = "Update production order")]
        [SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<Result<UpdateProductionResponse>>))]
        public async Task<IActionResult> UpdateProduction([FromBody] UpdateProductionRequest updateRequest, CancellationToken cancellationToken)
        {
            Result<UpdateProductionResponse> result = await _mediator.Send(updateRequest, cancellationToken);
            return await GetResponseFromResult(result);
        }

        [HttpGet("{orderId}")]
        [SwaggerOperation(Summary = "Get production order")]
        [SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<Result<GetProductionResponse>>))]
        public async Task<IActionResult> GetProduction([FromRoute] int orderId, CancellationToken cancellationToken)
        {
            Result<GetProductionResponse> result = await _mediator.Send(new GetProductionRequest(orderId), cancellationToken);
            return await GetResponseFromResult(result);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all productions")]
        [SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<Result<GetAllProductionResponse>>))]
        public async Task<IActionResult> GetAllProductions(CancellationToken cancellationToken)
        {
            Result<GetAllProductionResponse> result = await _mediator.Send(new GetAllProductionRequest(), cancellationToken);
            return await GetResponseFromResult(result);
        }

        [HttpGet("status/{status}")]
        [SwaggerOperation(Summary = "Get all productions by status")]
        [SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<Result<GetProductionByStatusResponse>>))]
        public async Task<IActionResult> GetProductionsByStatus([FromRoute] ProductionStatus status, CancellationToken cancellationToken)
        {
            Result<GetProductionByStatusResponse> result = await _mediator.Send(new GetProductionByStatusRequest(status), cancellationToken);
            return await GetResponseFromResult(result);
        }
    }
}
