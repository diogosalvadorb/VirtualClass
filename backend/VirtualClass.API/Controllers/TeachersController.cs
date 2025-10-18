using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualClass.Application.Commands.TeacherCommands.CreateTeacher;
using VirtualClass.Application.Commands.TeacherCommands.UpdateTeacher;
using VirtualClass.Application.Queries.TeacherQueries.GetAllTeachers;
using VirtualClass.Application.Queries.TeacherQueries.GetTeacherById;
using VirtualClass.Core.Results;

namespace VirtualClass.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TeachersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var query = new GetAllTeachersQuery();
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return StatusCode(500, new { message = result.Message });
            }

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(Guid id)
        {
            var query = new GetTeacherByIdQuery(id);
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return result.ErrorTypeEnum switch
                {
                    ErrorTypeEnum.NotFound => NotFound(new { message = result.Message }),
                    _ => StatusCode(500, new { message = result.Message })
                };
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] CreateTeacherCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return result.ErrorTypeEnum switch
                {
                    ErrorTypeEnum.NotFound => NotFound(new { message = result.Message }),
                    ErrorTypeEnum.Conflict => Conflict(new { message = result.Message }),
                    ErrorTypeEnum.Validation => BadRequest(new { message = result.Message }),
                    _ => StatusCode(500, new { message = result.Message })
                };
            }

            return CreatedAtAction(
                nameof(GetTeacherById),
                new { id = result.Data!.Id },
                new
                {
                    message = "Professor criado com sucesso!",
                    data = result.Data
                });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateTeacher(Guid id, [FromBody] UpdateTeacherCommand command)
        {
            command.TeacherId = id;
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return result.ErrorTypeEnum switch
                {
                    ErrorTypeEnum.NotFound => NotFound(new { message = result.Message }),
                    ErrorTypeEnum.Validation => BadRequest(new { message = result.Message }),
                    _ => StatusCode(500, new { message = result.Message })
                };
            }

            return Ok(new { message = "Professor atualizado com sucesso!" });
        }
    }
}
