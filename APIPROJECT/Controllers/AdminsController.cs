using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPROJECT.Models;
using APIPROJECT.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Text;
using System.Security.Cryptography;

namespace APIPROJECT.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;

        public AdminsController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            try
            {
                var admins = await _adminRepository.GetAdmins();
                var adminDtos = admins.Select(a => new Admin
                {
                    Admin_Id = a.Admin_Id,
                    Admin_Name = a.Admin_Name,
                    Admin_Email = a.Admin_Email,
                    Admin_Password = HashPassword(a.Admin_Password)
                }).ToList();

                return adminDtos;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while retrieving admins.");
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
        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            try
            {
                var admin = await _adminRepository.GetAdminById(id);
                if (admin == null)
                {
                    return NotFound();
                }
                return admin;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT: api/Admins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, Admin admin)
        {
            try
            {
                var result = await _adminRepository.UpdateAdmin(id, admin);
                if (!result)
                {
                    return BadRequest();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            try
            {
                var createdAdmin = await _adminRepository.AddAdmin(admin);
                return CreatedAtAction("GetAdmin", new { id = createdAdmin.Admin_Id }, createdAdmin);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            try
            {
                var result = await _adminRepository.DeleteAdmin(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
