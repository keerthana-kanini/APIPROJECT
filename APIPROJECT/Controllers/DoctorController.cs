using APIPROJECT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace APIPROJECT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DPContext _DContext;

        public DoctorController(DPContext Context)
        {
            _DContext = Context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> Get()
        {
            try
            {
                return await _DContext.Doctors.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> Get(int id)
        {
            try
            {
                Doctor dt = await _DContext.Doctors.SingleOrDefaultAsync(x => x.Doctor_Id == id);
                if (dt == null)
                {
                    return NotFound();
                }
                return dt;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Doctor>> Post(Doctor dt)
        {
            try
            {
                await _DContext.Doctors.AddAsync(dt);
                _DContext.SaveChanges();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Doctor dt)
        {
            try
            {
                if (id != dt.Doctor_Id)
                {
                    return BadRequest("Doctor Id mismatched!");
                }

                _DContext.Entry(dt).State = EntityState.Modified;
                await _DContext.SaveChangesAsync();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var dp = await _DContext.Doctors.FindAsync(id);

                if (dp == null)
                {
                    return NotFound();
                }

                _DContext.Doctors.Remove(dp);
                await _DContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                int count = await _DContext.Doctors.CountAsync();
                return Ok(count + " " + "Doctors are Available");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("{id}/patients/count")]
        public async Task<ActionResult<int>> GetPatientCountByDoctorId(int id)
        {
            try
            {
                if (_DContext.Doctors == null)
                {
                    return NotFound();
                }

                int count = await _DContext.Patients.CountAsync(p => p.doctor.Doctor_Id == id);
                return Ok(id + " Id Doctor " + " has " + count + " patients.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
