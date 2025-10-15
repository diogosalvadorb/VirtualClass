using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualClass.Application.Commands.UserCommands.ConfirmEmail;
using VirtualClass.Application.Commands.UserCommands.CreateUser;
using VirtualClass.Application.Commands.UserCommands.LoginUser;
using VirtualClass.Application.Queries.UserQueries.GetUserById;

namespace VirtualClass.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery(id);

            var user = await _mediator.Send(query);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var user = await _mediator.Send(command);

            return Ok(user);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var login = await _mediator.Send(command);
            if (login == null)
            {
                return BadRequest();
            }
            return Ok(login);
        }

        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
        {
            var command = new ConfirmEmailCommand(token);
            var result = await _mediator.Send(command);
            if (!result)
            {
                return BadRequest("Token inválido ou expirado.");
            }

            return Ok(new { message = "Email confirmado com sucesso! Você já pode fazer login." });
        }
    }
}
