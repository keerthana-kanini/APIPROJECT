using APIPROJECT.Models;

namespace APIPROJECT.Repository
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetPatients();
        Task<Patient> GetPatientById(int id);
        Task<Patient> AddPatient(Patient patient);
        Task<bool> UpdatePatient(int id, Patient patient);
        Task<bool> DeletePatient(int id);
        Task<IEnumerable<Doctor>> GetDoctorsBySpecialization(string specialization);
    }

}
