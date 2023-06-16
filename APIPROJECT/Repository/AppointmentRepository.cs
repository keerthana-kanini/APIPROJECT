using APIPROJECT.Models;
using Microsoft.EntityFrameworkCore;

namespace APIPROJECT.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DPContext _context;

        public AppointmentRepository(DPContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAppointments()
        {
            return await _context.Appointments
                .Include(x =>x.Patient)
                .Include(x => x.Doctor)
                .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentById(int id)
        {
            return await _context.Appointments.FindAsync(id);
        }

        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<bool> UpdateAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Appointment_Id)
            {
                return false;
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return false;
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Appointment_Id == id);
        }
    }

}
