using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualClass.Application.Commands.CourseCommands.CreateCourse;
using VirtualClass.Application.Commands.CourseCommands.UpdateCourse;
using VirtualClass.Application.Queries.CourseQueries.GetAllCourses;
using VirtualClass.Application.Queries.CourseQueries.GetCourseById;
using VirtualClass.Core.Results;

namespace VirtualClass.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCourses()
        {
            var query = new GetAllCoursesQuery();
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return StatusCode(500, new { message = result.Message });
            }

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            var query = new GetCourseByIdQuery(id);
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
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
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
                nameof(GetCourseById),
                new { id = result.Data!.Id },
                new
                {
                    message = "Curso criado com sucesso!",
                    data = result.Data
                });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateCourse(Guid id, [FromBody] UpdateCourseCommand command)
        {
            command.CourseId = id;
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

            return Ok(new { message = "Curso atualizado com sucesso!" });
        }          
    }
}