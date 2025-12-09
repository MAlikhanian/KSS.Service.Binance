using KSS.Service.Application.Features.FuturesOrder.Queries.GetOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KSS.Service.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FuturesOrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FuturesOrderController> _logger;

    public FuturesOrderController(
        IMediator mediator,
        ILogger<FuturesOrderController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<GetOrderResponse>> GetOrder(
        [FromQuery] string symbol,
        [FromQuery] long? orderId = null,
        [FromQuery] string? clientOrderId = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetOrderQuery
        {
            Symbol = symbol,
            OrderId = orderId,
            ClientOrderId = clientOrderId
        };

        var response = await _mediator.Send(query, cancellationToken);
        
        if (!response.Success)
        {
            if (response.ErrorMessage == "Order not found")
            {
                return NotFound(response);
            }
            return BadRequest(response);
        }

        return Ok(response);
    }
}

