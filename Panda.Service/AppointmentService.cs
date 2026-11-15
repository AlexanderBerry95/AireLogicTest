using Microsoft.EntityFrameworkCore;
using Panda.Business;
using Panda.Service.Models;

namespace Panda.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IPandaContext pandaContext;

        public AppointmentService(IPandaContext pandaContext)
        {
            this.pandaContext = pandaContext;
        }

        public async Task<int> CreateAppointment(AppointmentModel request)
        {
            bool patientExists = await pandaContext.Patients
                .AnyAsync(patient => patient.Id == request.PatientId);

            if (!patientExists)
            {
                throw new InvalidOperationException(
                    $"No patient found with ID: {request.PatientId}");
            }

            var appointment = new Appointment
            {
                Status = request.Status.Trim(),
                Text = request.Text.Trim(),
                Duration = request.Duration.Trim(),
                Clinician = request.Clinician.Trim(),
                Department = request.Department.Trim(),
                PostCode = request.PostCode.Trim(),
                PatientId = request.PatientId
            };

            pandaContext.Appointments.Add(appointment);
            await pandaContext.SaveChangesAsync();

            return appointment.Id;        
        }

        public async Task<AppointmentModel> GetAppointmentById(int id)
        {
            var appointment = await pandaContext.Appointments
                .FirstOrDefaultAsync(item => item.Id == id) ?? throw new InvalidOperationException($"No appointment found with ID: {id}");

            return new AppointmentModel
            {
                Id = appointment.Id,
                Status = appointment.Status,
                Text = appointment.Text,
                Duration = appointment.Duration,
                Clinician = appointment.Clinician,
                Department = appointment.Department,
                PostCode = appointment.PostCode,
                PatientId = appointment.PatientId
            };
        }

        public async Task UpdateAppointment(AppointmentModel appointmentModel)
        {
            var appointment = await pandaContext.Appointments
                .AsNoTracking()
                .FirstOrDefaultAsync(item => item.Id == appointmentModel.Id) ?? throw new InvalidOperationException($"No appointment found with ID: {appointmentModel.Id}");

            if (appointment.Status == "Cancelled" && appointmentModel.Status != "Cancelled")
            {
                throw new InvalidOperationException($"Appointment with ID: {appointmentModel.Id} is cancelled - it's status cannot be updated.");
            }

            Appointment updatedAppointment = appointment with
            {
                Status = appointmentModel.Status.Trim(),
                Text = appointmentModel.Text.Trim(),
                Duration = appointmentModel.Duration.Trim(),
                Clinician = appointmentModel.Clinician.Trim(),
                Department = appointmentModel.Department.Trim(),
                PostCode = appointmentModel .PostCode.Trim(),
                PatientId = appointmentModel.PatientId
            };
            
            pandaContext.Appointments.Update(updatedAppointment);
            await pandaContext.SaveChangesAsync();
        }

        public async Task DeleteAppointment(int id)
        {
            var appointment = await pandaContext.Appointments
                .FirstOrDefaultAsync(item => item.Id == id) ?? throw new InvalidOperationException($"No appointment found with ID: {id}");

            if(appointment.Status == "Cancelled")
            {
                throw new InvalidOperationException($"Appointment with ID: {id} is already cancelled.");
            }

            Appointment updatedAppointment = appointment with
            {
                Status = "Cancelled",
                Text = appointment.Text.Trim(),
                Duration = appointment.Duration.Trim(),
                Clinician = appointment.Clinician.Trim(),
                Department = appointment.Department.Trim(),
                PostCode = appointment.PostCode.Trim(),
                PatientId = appointment.PatientId
            };

            pandaContext.Appointments.Remove(appointment);
            await pandaContext.SaveChangesAsync();
        }
    }
}
