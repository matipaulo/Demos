using System;
using CancellationTokenSource.Api.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokenSource.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LongTaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LongTaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Index([FromQuery] string name, CancellationToken token)
        {
            try
            {
                await _mediator.Send(new LongTaskRequest(name), token);
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Task canceled.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation canceled.");
            }
            
            return Ok();
        }
    }
}