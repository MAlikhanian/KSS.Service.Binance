namespace KSS.Service.API.Controllers;

[Route("api/[controller]")]
public class FuturesOrderController : BaseController
{
    private readonly IMediator _mediator;

    public FuturesOrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrder([FromQuery] GetOrderRequest request)
    {
        var query = request.Adapt<GetOrderQuery>();
        var result = await _mediator.Send(query);
        
        if (!result.Success)
        {
            if (result.ErrorMessage == "Order not found")
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }
        
        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersRequest request)
    {
        var query = request.Adapt<GetAllOrdersQuery>();
        var result = await _mediator.Send(query);
        
        if (!result.Success)
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> NewOrder([FromBody] NewOrderRequest request)
    {
        var command = request.Adapt<NewOrderCommand>();
        var result = await _mediator.Send(command);
        
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("market-buy")]
    public async Task<IActionResult> NewMarketBuyOrder([FromBody] NewMarketBuyOrderRequest request)
    {
        var command = request.Adapt<NewMarketBuyOrderCommand>();
        var result = await _mediator.Send(command);
        
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("market-sell")]
    public async Task<IActionResult> NewMarketSellOrder([FromBody] NewMarketSellOrderRequest request)
    {
        var command = request.Adapt<NewMarketSellOrderCommand>();
        var result = await _mediator.Send(command);
        
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("limit-buy")]
    public async Task<IActionResult> NewLimitBuyOrder([FromBody] NewLimitBuyOrderRequest request)
    {
        var command = request.Adapt<NewLimitBuyOrderCommand>();
        var result = await _mediator.Send(command);
        
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("limit-sell")]
    public async Task<IActionResult> NewLimitSellOrder([FromBody] NewLimitSellOrderRequest request)
    {
        var command = request.Adapt<NewLimitSellOrderCommand>();
        var result = await _mediator.Send(command);
        
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("multiple")]
    public async Task<IActionResult> NewMultipleOrders([FromBody] NewMultipleOrdersRequest request)
    {
        var command = request.Adapt<NewMultipleOrdersCommand>();
        var result = await _mediator.Send(command);
        
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete]
    public async Task<IActionResult> CancelOrder([FromQuery] CancelOrderRequest request)
    {
        var command = request.Adapt<CancelOrderCommand>();
        var result = await _mediator.Send(command);
        
        if (!result.Success)
        {
            if (result.ErrorMessage?.Contains("not found") == true)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }
        
        return Ok(result);
    }

    [HttpDelete("all")]
    public async Task<IActionResult> CancelAllOpenOrders([FromQuery] CancelAllOpenOrdersRequest request)
    {
        var command = request.Adapt<CancelAllOpenOrdersCommand>();
        var result = await _mediator.Send(command);
        
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("multiple")]
    public async Task<IActionResult> CancelMultipleOrders([FromBody] CancelMultipleOrdersRequest request)
    {
        var command = request.Adapt<CancelMultipleOrdersCommand>();
        var result = await _mediator.Send(command);
        
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> ModifyOrder([FromBody] ModifyOrderRequest request)
    {
        var command = request.Adapt<ModifyOrderCommand>();
        var result = await _mediator.Send(command);
        
        if (!result.Success)
        {
            if (result.ErrorMessage?.Contains("not found") == true)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }
        
        return Ok(result);
    }

    [HttpPut("multiple")]
    public async Task<IActionResult> ModifyMultipleOrders([FromBody] ModifyMultipleOrdersRequest request)
    {
        var command = request.Adapt<ModifyMultipleOrdersCommand>();
        var result = await _mediator.Send(command);
        
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}
