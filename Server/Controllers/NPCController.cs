using Kobold.Server.Features.NPC.Commands;
using Kobold.Server.Features.NPC.Queries;
using Kobold.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kobold.Server.Controllers;

public class NPCController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NPC>>> GetAll()
    {
        var npcs = await Mediator.Send(new GetAllNpcsQuery());
        return Ok(npcs);
    }
    
    [HttpPost]
    public async Task<ActionResult> Add(AddCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}