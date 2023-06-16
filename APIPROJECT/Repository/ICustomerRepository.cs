using APIPROJECT.Models;

namespace APIPROJECT.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(int id);
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> UpdateCustomer(int id, Customer customer);
        Task<bool> DeleteCustomer(int id);
        bool CustomerExists(int id);
    }
}
