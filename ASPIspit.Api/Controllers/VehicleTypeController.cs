using ASPIspit.Application.UseCases.Commands;
using ASPIspit.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPIspit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleTypeController : ControllerBase
    {

        // DELETE api/<ServiceTypeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteVehicleTypeCommand command, [FromServices] UseCaseHandler handler)
        {
            handler.HandleCommand(command, id);

            return NoContent();
        }
    }
}
