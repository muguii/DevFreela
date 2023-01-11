using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            this._projectService = projectService;
        }

        [HttpGet]
        public IActionResult Get(string query)
        {
            return Ok(_projectService.GetAll(query));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _projectService.GetById(id);

            return project != null ? Ok(project) : NotFound();  
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewProjectInputModel projectInputModel)
        {
            if (projectInputModel == null)
                return BadRequest();

            int id = _projectService.Create(projectInputModel);

            return CreatedAtAction(nameof(GetById), new { id = id }, projectInputModel);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectInputModel projectInputModel)
        {
            if (projectInputModel == null)
                return BadRequest();

            _projectService.Update(projectInputModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _projectService.Delete(id);

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, [FromBody] CreateCommentInputModel commentInputModel)
        {
            _projectService.CreateComment(commentInputModel);
            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            _projectService.Start(id);
            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public IActionResult Finish(int id)
        {
            _projectService.Finish(id);
            return NoContent();
        }
    }
}
