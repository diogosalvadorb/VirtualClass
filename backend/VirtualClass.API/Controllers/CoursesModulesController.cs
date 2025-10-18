using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualClass.Application.Commands.CourseModuleCommands.CreateCourseModule;
using VirtualClass.Application.Commands.CourseModuleCommands.DeleteCourseModule;
using VirtualClass.Application.Commands.CourseModuleCommands.UpdateCourseModule;
using VirtualClass.Application.Queries.CourseModuleQueries.GetCourseModuleById;
using VirtualClass.Core.Results;

namespace VirtualClass.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesModulesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CoursesModulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModuleById(Guid id)
        {
            var query = new GetCourseModuleByIdQuery(id);
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
        public async Task<IActionResult> CreateModule([FromBody] CreateCourseModuleCommand command)
        {
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

            return CreatedAtAction(
                nameof(GetModuleById),
                new { id = result.Data!.Id },
                new
                {
                    message = "Módulo criado com sucesso!",
                    data = result.Data
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModule(Guid id, [FromBody] UpdateCourseModuleCommand command)
        {
            command.ModuleId = id;
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

            return Ok(new { message = "Módulo atualizado com sucesso!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(Guid id)
        {
            var command = new DeleteCourseModuleCommand(id);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return result.ErrorTypeEnum switch
                {
                    ErrorTypeEnum.NotFound => NotFound(new { message = result.Message }),
                    _ => StatusCode(500, new { message = result.Message })
                };
            }

            return Ok(new { message = "Módulo excluído com sucesso!" });
        }
    }
}
