using APIPROJECT.Models;
using Microsoft.EntityFrameworkCore;

namespace APIPROJECT.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DPContext _context;

        public PatientRepository(DPContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetPatients()
        {
            return await _context.Patients.Include(x => x.doctor).ToListAsync();
        }

        public async Task<Patient> GetPatientById(int id)
        {
            return await _context.Patients.Include(x => x.doctor).FirstOrDefaultAsync(x => x.Patient_Id == id);
        }

        public async Task<Patient> AddPatient(Patient patient)
        {
            Doctor dt = await _context.Doctors.FindAsync(patient.doctor.Doctor_Id);
            patient.doctor = dt;
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<bool> UpdatePatient(int id, Patient patient)
        {
            if (id != patient.Patient_Id)
            {
                return false;
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        public async Task<bool> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return false;
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsBySpecialization(string specialization)
        {
            return await _context.Doctors.Where(d => d.Specialization == specialization).ToListAsync();
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Patient_Id == id);
        }
    }

}
