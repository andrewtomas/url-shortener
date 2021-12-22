using Application.Common.Models;
using Application.Features.UrlGroups;
using Application.Features.URLGroups;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("groups")]
[ApiController]
public class UrlGroupsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UrlGroupsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup(CreateUrlGroupCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToResponse();
    }

    [HttpGet]
    public async Task<ActionResult<CQRSResponse>> GetAllGroups([FromQuery] GetAllGroupsQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CQRSResponse>> DeleteGroup(string id)
    {
        return await _mediator.Send(new DeleteUrlGroupCommand {Id = id});
    }
}