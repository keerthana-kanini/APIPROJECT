using APIPROJECT.Models;

public interface IDoctorRepository
{
    Task<IEnumerable<Doctor>> GetDoctors();
    Task<Doctor> GetDoctorById(int id);
    Task<Doctor> AddDoctor(Doctor doctor);
    Task<bool> UpdateDoctor(int id, Doctor doctor);
    Task<bool> DeleteDoctor(int id);
    Task<int> GetDoctorCount();
    Task<int> GetPatientCountByDoctorId(int id);
}
