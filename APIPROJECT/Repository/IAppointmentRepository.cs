using APIPROJECT.Models;

namespace APIPROJECT.Repository
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAppointments();
        Task<Appointment> GetAppointmentById(int id);
        Task<Appointment> AddAppointment(Appointment appointment);
        Task<bool> UpdateAppointment(int id, Appointment appointment);
        Task<bool> DeleteAppointment(int id);
    }

}
