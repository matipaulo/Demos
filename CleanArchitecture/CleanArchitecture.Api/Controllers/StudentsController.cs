using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Application.Commands;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StudentDto>> CreateStudent([FromBody] CreateStudentCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<StudentDto>>> GetAll([FromQuery] GetStudentsByNameQuery query)
        {
            var students = await _mediator.Send(query);

            return Ok(students);
        }
    }
}