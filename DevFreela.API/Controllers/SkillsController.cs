using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetAllSkillsQuery query = new GetAllSkillsQuery();
            List<SkillViewModel> skills = await _mediator.Send(query);

            return Ok(skills);
        }
    }
}
