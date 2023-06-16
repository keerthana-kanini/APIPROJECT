using APIPROJECT.Models;
using Microsoft.EntityFrameworkCore;

namespace APIPROJECT.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DPContext _context;

        public DoctorRepository(DPContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            return await _context.Doctors.Include(x => x.Patients).ToListAsync();
        }

        public async Task<Doctor> GetDoctorById(int id)
        {
            return await _context.Doctors.SingleOrDefaultAsync(x => x.Doctor_Id == id);
        }

        public async Task<Doctor> AddDoctor(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task<bool> UpdateDoctor(int id, Doctor doctor)
        {
            if (id != doctor.Doctor_Id)
            {
                return false;
            }

            _context.Entry(doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return false;
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetDoctorCount()
        {
            return await _context.Doctors.CountAsync();
        }

        public async Task<int> GetPatientCountByDoctorId(int id)
        {
            return await _context.Patients.CountAsync(p => p.doctor.Doctor_Id == id);
        }
    }

}
