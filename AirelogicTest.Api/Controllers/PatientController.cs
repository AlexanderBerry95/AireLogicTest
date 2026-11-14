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
        public async Task<ActionResult<Patient>> CreatePatient([FromBody] CreatePatientRequest request)
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
                return BadRequest("An error occurred while creating the patient."); //TODO: Update this!
            }
        }
    }
}