using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly OpeningTimeOption option;
        private readonly ExampleClass exampleClass;

        public ProjectsController(IOptions<OpeningTimeOption> option, ExampleClass exampleClass)
        {
            this.option = option.Value;

            exampleClass.Name = "Update at ProjectsController";
            this.exampleClass = exampleClass;
        }

        [HttpGet] //api/projects?query=net core
        public IActionResult Get(string query)
        {
            // Buscar todos projetos e/ou filtrar

            return Ok();
        }

        [HttpGet("{id}")] //api/projects/1
        public IActionResult GetById(int id)
        {
            // return NotFound();

            // Buscar o projeto

            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateProjectModel createProject)
        {
            if (createProject.Title.Length > 50)
            {
                return BadRequest();
            }

            // Criar o projeto

            return CreatedAtAction(nameof(GetById), new { id = createProject.Id }, createProject);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectModel updateProject)
        {
            if (updateProject.Description.Length > 200)
            {
                return BadRequest();
            }

            // Atualiza o projeto

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Buscar, se não existir, NotFound()

            // Remover o projeto

            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, [FromBody] CreateCommentModel createComment)
        {
            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public IActionResult Finish(int id)
        {
            return NoContent();
        }
    }
}
