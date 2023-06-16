using System.ComponentModel.DataAnnotations;

namespace APIPROJECT.Models
{
    public class Doctor
    {
        [Key]
        public int Doctor_Id { get; set; }

        public string ? Doctor_Name { get; set; }

        public string ?  Specialization { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? Doctor_No { get; set; }

        public ICollection<Patient>? Patients { get; set; }

    }
}
