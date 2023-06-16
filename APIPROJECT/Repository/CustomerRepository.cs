using APIPROJECT.Models;
using Microsoft.EntityFrameworkCore;

namespace APIPROJECT.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DPContext _context;

        public CustomerRepository(DPContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            try
            {
                return await _context.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error occurred while retrieving customers.", ex);
            }
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            try
            {
                return await _context.Customers.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error occurred while retrieving the customer.", ex);
            }
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error occurred while creating the customer.", ex);
            }
        }

        public async Task<Customer> UpdateCustomer(int id, Customer customer)
        {
            try
            {
                _context.Entry(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return customer;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error occurred while updating the customer.", ex);
            }
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return false;
                }

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error occurred while deleting the customer.", ex);
            }
        }

        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Customer_Id == id);
        }
    }
}
