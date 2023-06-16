using APIPROJECT.Models;

namespace APIPROJECT.Repository
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admin>> GetAdmins();
        Task<Admin> GetAdminById(int id);
        Task<Admin> AddAdmin(Admin admin);
        Task<bool> UpdateAdmin(int id, Admin admin);
        Task<bool> DeleteAdmin(int id);
    }

}
