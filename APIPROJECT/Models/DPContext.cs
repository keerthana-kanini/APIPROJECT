using Microsoft.EntityFrameworkCore;

namespace APIPROJECT.Models
{
    public class DPContext :DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<User> Users { get; set; }


        public DPContext(DbContextOptions<DPContext> options) : base(options)
        {

        }
    }
}
