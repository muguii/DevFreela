using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "client,freelancer")]
        public async Task<IActionResult> Get(string query)
        {
            return Ok(await _mediator.Send(new GetAllProjectsQuery(query)));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "client,freelancer")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _mediator.Send(new GetProjectByIdQuery(id));

            return project != null ? Ok(project) : NotFound();  
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand createProjectCommand)
        {
            if (createProjectCommand == null)
                return BadRequest();

            var id = await _mediator.Send(createProjectCommand);

            return CreatedAtAction(nameof(GetById), new { id }, createProjectCommand);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand updateProjectCommand)
        {
            if (updateProjectCommand == null)
                return BadRequest();

            await _mediator.Send(updateProjectCommand);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteProjectCommand(id));

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand createCommentCommand)
        {
            await _mediator.Send(createCommentCommand);
            return NoContent();
        }

        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Start(int id)
        {
            await _mediator.Send(new StartProjectCommand(id));
            return NoContent();
        }

        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Finish(int id, [FromBody] FinishProjectCommand finishProjectCommand)
        {
            finishProjectCommand.Id = id;
            var result = await _mediator.Send(finishProjectCommand);

            return Accepted();
        }
    }
}
