using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VirtualClass.Application.Commands.UserCommands.ChangePassword;
using VirtualClass.Application.Commands.UserCommands.ConfirmEmail;
using VirtualClass.Application.Commands.UserCommands.CreateUser;
using VirtualClass.Application.Commands.UserCommands.ForgotPassword;
using VirtualClass.Application.Commands.UserCommands.LoginUser;
using VirtualClass.Application.Commands.UserCommands.RecoverPassword;
using VirtualClass.Application.Queries.UserQueries.GetUserById;
using VirtualClass.Core.Results;

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
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return result.ErrorTypeEnum switch
                {
                    ErrorTypeEnum.Validation => BadRequest(new { message = result.Message }),
                    _ => StatusCode(500, new { message = result.Message })
                };
            }

            return Ok(new
            {
                message = "Usuário criado com sucesso! Verifique seu email para confirmar o cadastro.",
                data = result.Data
            });
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

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(new { message = "Usuário não autenticado." });
            }
           
            command.Email = userEmail;

            if (string.IsNullOrEmpty(command.CurrentPassword) || string.IsNullOrEmpty(command.NewPassword))
            {
                return BadRequest(new { message = "Senha atual e nova senha são obrigatórias." });
            }

            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest(new { message = "Senha atual incorreta." });
            }

            return Ok(new { message = "Senha alterada com sucesso!" });
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            await _mediator.Send(command);

            return Ok(new
            {
                message = "Se o email existir em nossa base, você receberá instruções para redefinir sua senha."
            });
        }

        [HttpPost("recover-password")]
        [AllowAnonymous]
        public async Task<IActionResult> RecoverPassword([FromBody] RecoverPasswordCommand command)
        {
            if (string.IsNullOrEmpty(command.Token) || string.IsNullOrEmpty(command.NewPassword))
            {
                return BadRequest(new { message = "Token e nova senha são obrigatórios." });
            }

            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest(new { message = "Token inválido ou expirado." });
            }

            return Ok(new { message = "Senha recuperada com sucesso! Você já pode fazer login." });
        }
    }
}
