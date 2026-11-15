using Panda.Service.Models;

namespace Panda.Service
{
    public interface IAppointmentService
    {
        Task<int> CreateAppointment(AppointmentModel request);
        Task<bool> DeleteAppointment(int id);
        Task<AppointmentModel> GetAppointmentModelById(int id);
    }
}