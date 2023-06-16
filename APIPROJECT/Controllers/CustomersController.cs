using APIPROJECT.Models;
using APIPROJECT.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace APIPROJECT.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]

    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetCustomers();
                var customerDtos = customers.Select(c => new Customer
                {
                    Customer_Id = c.Customer_Id,
                    Customer_Name = c.Customer_Name,
                    Customer_Email = c.Customer_Email,
                    Customer_Password = HashPassword(c.Customer_Password)
                }).ToList();

                return customerDtos;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while retrieving customers.");
            }
        }


        private string HashPassword(string password)
        {

            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerById(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return customer;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while retrieving the customer.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Post([FromBody] Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var createdCustomer = await _customerRepository.CreateCustomer(customer);
                return CreatedAtAction(nameof(Get), new { id = createdCustomer.Customer_Id }, createdCustomer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while creating the customer.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            try
            {
                if (!ModelState.IsValid || id != customer.Customer_Id)
                {
                    return BadRequest();
                }

                var updatedCustomer = await _customerRepository.UpdateCustomer(id, customer);
                if (updatedCustomer == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while updating the customer.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var isDeleted = await _customerRepository.DeleteCustomer(id);
                if (!isDeleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while deleting the customer.");
            }
        }
    }
}
