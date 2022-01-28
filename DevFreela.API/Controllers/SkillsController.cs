using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [Authorize]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> Get()
        {
            GetAllSkillsQuery query = new GetAllSkillsQuery();
            List<SkillDTO> skills = await _mediator.Send(query);

            return Ok(skills);
        }
    }
}
