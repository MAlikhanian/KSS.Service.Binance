using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KSS.Service.API.Common;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;

    protected BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
}

