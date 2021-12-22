using Application.Common.Models;
using Application.Features.UrlItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
public class UrlItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UrlItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/shorten")]
    public async Task<IActionResult> Create(CreateUrlItemCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToResponse();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CQRSResponse>> Delete(string id)
    {
        return await _mediator.Send(new DeleteUrlItemCommand {Id = id});
    }

    // Get
    // Update
    // Delete
    //[HttpDelete()]
    //public async Task<IActionResult> Delete()
    //{
    //    return null;
    //}
}