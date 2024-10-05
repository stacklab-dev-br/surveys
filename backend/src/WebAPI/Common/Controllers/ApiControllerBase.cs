using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StackLab.Survey.WebAPI.Common.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}