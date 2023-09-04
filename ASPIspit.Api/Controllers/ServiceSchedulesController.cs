using ASPIspit.Application.DTO;
using ASPIspit.Application.UseCases.Commands;
using ASPIspit.Application.UseCases.Queries;
using ASPIspit.Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPIspit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceSchedulesController : ControllerBase
    {
        private UseCaseHandler handler;

        public ServiceSchedulesController(UseCaseHandler handler)
        {
            this.handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] ServiceScheduleSearch search,
                                 [FromServices] IGetServicescheduleQuery query)
        {
            return Ok(handler.HandleQuery(query, search));
        }
        // PUT api/<ServiceScheduleController>/5
        [HttpPut("/api/serviceschedule/finish")]
        public IActionResult Put([FromBody] FInishServiceDto dto,
                                 [FromServices] IFinishServiceCommand command)
        {
            handler.HandleCommand(command, dto);

            return NoContent();
        }

       
    }
}
