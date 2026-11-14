using Microsoft.AspNetCore.Mvc;
using Panda.Business;
using Panda.Service;
using Panda.Service.Models;

namespace Panda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;

        public PatientController(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Patient>> CreatePatient([FromBody] PatientModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }

                Patient createdPatient = await patientService.CreatePatient(request);
                return Ok(createdPatient.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientModel>> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid patient ID.");
                }

                var patient = await patientService.GetPatientModelById(id);
                if (patient == null)
                {
                    return NotFound("No patient found with ID: " + id);
                }

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid patient ID.");
                }

                await patientService.DeletePatient(id);
                return Ok("Patient deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}