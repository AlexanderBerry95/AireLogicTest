using Microsoft.AspNetCore.Mvc;
using Panda.Service;
using Panda.Service.Models;

namespace Panda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<int>> CreateAppointment([FromBody] AppointmentModel request)
        {
            try
            {
                int createdAppointmentId = await appointmentService.CreateAppointment(request);
                return Ok(createdAppointmentId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentModel>> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid appointment ID.");
                }

                AppointmentModel appointment = await appointmentService.GetAppointmentById(id);
                if (appointment == null)
                {
                    return NotFound("No appointment found with ID: " + id);
                }
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteAppointment(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid appointment ID.");
                }

                await appointmentService.DeleteAppointment(id);
                return Ok("Appointment deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("Update")]
        public async Task<ActionResult> UpdateAppointment([FromBody] AppointmentModel request)
        {
            try
            {
                await appointmentService.UpdateAppointment(request);
                return Ok("Appointment updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
