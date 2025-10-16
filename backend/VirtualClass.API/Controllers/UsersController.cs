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
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return result.ErrorTypeEnum switch
                {
                    ErrorTypeEnum.NotFound => NotFound(new { message = result.Message }),
                    _ => BadRequest(new { message = result.Message })
                };
            }

            return Ok(result.Data);
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
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return result.ErrorTypeEnum switch
                {
                    ErrorTypeEnum.Unauthorized => Unauthorized(new { message = result.Message }),
                    _ => StatusCode(500, new { message = result.Message })
                };
            }

            return Ok(result.Data);
        }

        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
        {
            var command = new ConfirmEmailCommand(token);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return result.ErrorTypeEnum switch
                {
                    ErrorTypeEnum.NotFound => BadRequest(new { message = result.Message }),
                    _ => BadRequest(new { message = result.Message })
                };
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

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return result.ErrorTypeEnum switch
                {
                    ErrorTypeEnum.Unauthorized => Unauthorized(new { message = result.Message }),
                    ErrorTypeEnum.Validation => BadRequest(new { message = result.Message }),
                    _ => StatusCode(500, new { message = result.Message })
                };
            }

            return Ok(new { message = "Senha alterada com sucesso!" });
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return StatusCode(500, new { message = result.Message });
            }

            return Ok(new
            {
                message = "Email enviado! você receberá instruções para redefinir sua senha."
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

            if (!result.IsSuccess)
            {
                return result.ErrorTypeEnum switch
                {
                    ErrorTypeEnum.NotFound => BadRequest(new { message = result.Message }),
                    ErrorTypeEnum.Validation => BadRequest(new { message = result.Message }),
                    _ => StatusCode(500, new { message = result.Message })
                };
            }

            return Ok(new { message = "Senha recuperada com sucesso! Você já pode fazer login." });
        }
    }
}
