using ASPIspit.Application.DTO;
using ASPIspit.Application.UseCases.Commands;
using ASPIspit.Implementation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPIspit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
       
        // POST api/<RegistrationController>
        [HttpPost]
        public IActionResult Post([FromBody] RegistrationDto dto,
                                   [FromServices] IAddRegistrationCommand command,
                                   [FromServices] UseCaseHandler handler)
        {
            handler.HandleCommand(command, dto);

            return StatusCode(201);
        }
    }
}
