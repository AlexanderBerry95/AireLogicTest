using Microsoft.AspNetCore.Mvc;
using Panda.Service.Interfaces;
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
        public async Task<ActionResult<int>> CreatePatient([FromBody] PatientModel request)
        {
            try
            {
                int createdPatientId = await patientService.CreatePatient(request);
                return Ok(createdPatientId);
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

        [HttpPut]
        public async Task<ActionResult> UpdatePatient([FromBody] PatientModel updateModel)
        {
            try
            {
                await patientService.UpdatePatient(updateModel);
                return Ok("Patient updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}