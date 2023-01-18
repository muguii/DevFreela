using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "client,freelancer")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery(id)));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand createUserCommand)
        {
            int id = await _mediator.Send(createUserCommand);
            return CreatedAtAction(nameof(GetById), new { id }, createUserCommand);
        }

        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            var userLogin = await _mediator.Send(loginUserCommand);

            if (userLogin == null)
                return BadRequest();

            return Ok(userLogin);
        }
    }
}
