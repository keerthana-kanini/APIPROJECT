using System.ComponentModel.DataAnnotations;

namespace APIPROJECT.Models
{
    public class Doctor
    {
        [Key]
        public int Doctor_Id { get; set; }

        public string ? Doctor_Name { get; set; }

        public string ?  Specialization { get; set; }

        public int Doctor_No { get; set; }

        public ICollection<Patient>? Patients { get; set; }

    }
}
