using DevFreela.Application.Queries.GetAllSkills;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkillsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "client,freelancer")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAllSkillsQuery()));
        }
    }
}
