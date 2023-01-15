using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand createUserCommand)
        {
            int id = await _mediator.Send(createUserCommand);
            return CreatedAtAction(nameof(GetById), new { id }, createUserCommand);
        }

        [HttpPut("{id}/login")]
        public IActionResult Login(int id, [FromBody] object loginModel)
        {
            return NoContent();
            // Token JWT
        }
    }
}
