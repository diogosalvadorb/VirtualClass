using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualClass.Application.Commands.CreateUser;
using VirtualClass.Application.Commands.LoginUser;

namespace VirtualClass.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}
