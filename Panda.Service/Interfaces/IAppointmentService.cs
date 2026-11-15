using Panda.Service.Models;

namespace Panda.Service
{
    public interface IAppointmentService
    {
        Task<int> CreateAppointment(AppointmentModel request);
        Task DeleteAppointment(int id);
        Task<AppointmentModel> GetAppointmentById(int id);
        Task UpdateAppointment(AppointmentModel appointmentModel);
    }
}