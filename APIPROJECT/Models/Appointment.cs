using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIPROJECT.Models
{
    public class Appointment
    {
        [Key]
        public int Appointment_Id { get; set; }

        [ForeignKey("Patient")]
        public int Patient_Id { get; set; }
        public Patient? Patient { get; set; }

        [ForeignKey("Doctor")]
        public int Doctor_Id { get; set; }
        public Doctor? Doctor { get; set; }

       
        public DateTime Appointment_Date { get; set; }
    }
}
