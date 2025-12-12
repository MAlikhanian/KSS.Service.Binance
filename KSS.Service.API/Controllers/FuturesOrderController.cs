using KSS.Service.Application.Features.FuturesOrder.Commands.CancelAllOpenOrders;
using KSS.Service.Application.Features.FuturesOrder.Commands.CancelMultipleOrders;
using KSS.Service.Application.Features.FuturesOrder.Commands.CancelOrder;
using KSS.Service.Application.Features.FuturesOrder.Commands.ModifyMultipleOrders;
using KSS.Service.Application.Features.FuturesOrder.Commands.ModifyOrder;
using KSS.Service.Application.Features.FuturesOrder.Commands.NewLimitBuyOrder;
using KSS.Service.Application.Features.FuturesOrder.Commands.NewLimitSellOrder;
using KSS.Service.Application.Features.FuturesOrder.Commands.NewMarketBuyOrder;
using KSS.Service.Application.Features.FuturesOrder.Commands.NewMarketSellOrder;
using KSS.Service.Application.Features.FuturesOrder.Commands.NewMultipleOrders;
using KSS.Service.Application.Features.FuturesOrder.Commands.NewOrder;
using KSS.Service.Application.Features.FuturesOrder.Queries.GetAllOrders;
using KSS.Service.Application.Features.FuturesOrder.Queries.GetOrder;

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

    [HttpGet("all")]
    public async Task<ActionResult<GetAllOrdersResponse>> GetAllOrders(
        [FromQuery] string symbol,
        [FromQuery] int? limit = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllOrdersQuery
        {
            Symbol = symbol,
            Limit = limit
        };

        var response = await _mediator.Send(query, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<NewOrderResponse>> NewOrder(
        [FromBody] NewOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("market-buy")]
    public async Task<ActionResult<NewMarketBuyOrderResponse>> NewMarketBuyOrder(
        [FromBody] NewMarketBuyOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("market-sell")]
    public async Task<ActionResult<NewMarketSellOrderResponse>> NewMarketSellOrder(
        [FromBody] NewMarketSellOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("limit-buy")]
    public async Task<ActionResult<NewLimitBuyOrderResponse>> NewLimitBuyOrder(
        [FromBody] NewLimitBuyOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("limit-sell")]
    public async Task<ActionResult<NewLimitSellOrderResponse>> NewLimitSellOrder(
        [FromBody] NewLimitSellOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("multiple")]
    public async Task<ActionResult<NewMultipleOrdersResponse>> NewMultipleOrders(
        [FromBody] NewMultipleOrdersCommand command,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult<CancelOrderResponse>> CancelOrder(
        [FromQuery] string symbol,
        [FromQuery] long? orderId = null,
        [FromQuery] string? clientOrderId = null,
        CancellationToken cancellationToken = default)
    {
        var command = new CancelOrderCommand
        {
            Symbol = symbol,
            OrderId = orderId,
            ClientOrderId = clientOrderId
        };

        var response = await _mediator.Send(command, cancellationToken);

        if (!response.Success)
        {
            if (response.ErrorMessage?.Contains("not found") == true)
            {
                return NotFound(response);
            }
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpDelete("all")]
    public async Task<ActionResult<CancelAllOpenOrdersResponse>> CancelAllOpenOrders(
        [FromQuery] string symbol,
        CancellationToken cancellationToken = default)
    {
        var command = new CancelAllOpenOrdersCommand
        {
            Symbol = symbol
        };

        var response = await _mediator.Send(command, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpDelete("multiple")]
    public async Task<ActionResult<CancelMultipleOrdersResponse>> CancelMultipleOrders(
        [FromBody] CancelMultipleOrdersCommand command,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<ModifyOrderResponse>> ModifyOrder(
        [FromBody] ModifyOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (!response.Success)
        {
            if (response.ErrorMessage?.Contains("not found") == true)
            {
                return NotFound(response);
            }
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPut("multiple")]
    public async Task<ActionResult<ModifyMultipleOrdersResponse>> ModifyMultipleOrders(
        [FromBody] ModifyMultipleOrdersCommand command,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}
