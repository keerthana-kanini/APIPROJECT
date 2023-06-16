using Microsoft.EntityFrameworkCore;

namespace APIPROJECT.Models
{
    public class DPContext :DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
   

        public DPContext(DbContextOptions<DPContext> options) : base(options)
        {

        }
    }
}
