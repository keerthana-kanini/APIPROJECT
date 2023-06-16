using APIPROJECT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace APIPROJECT.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> Get()
        {
            try
            {
                var doctors = await _doctorRepository.GetDoctors();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> Get(int id)
        {
            try
            {
                var doctor = await _doctorRepository.GetDoctorById(id);
                if (doctor == null)
                {
                    return NotFound();
                }
                return doctor;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Doctor>> Post(Doctor doctor)
        {
            try
            {
                var addedDoctor = await _doctorRepository.AddDoctor(doctor);
                return Ok(addedDoctor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Doctor doctor)
        {
            try
            {
                var result = await _doctorRepository.UpdateDoctor(id, doctor);
                if (!result)
                {
                    return BadRequest("Doctor Id mismatched!");
                }
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _doctorRepository.DeleteDoctor(id);
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
        [Authorize(Roles = "Customer,Admin")]

        [HttpGet("count")]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                int count = await _doctorRepository.GetDoctorCount();
                return Ok(count + " " + "Doctors are Available");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("{id}/patients/count")]
        public async Task<ActionResult<int>> GetPatientCountByDoctorId(int id)
        {
            try
            {
                int count = await _doctorRepository.GetPatientCountByDoctorId(id);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
