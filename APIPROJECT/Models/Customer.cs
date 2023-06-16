using System.ComponentModel.DataAnnotations;

namespace APIPROJECT.Models
{
    public class Customer
    {

        [Key]
        public int Customer_Id { get; set; }
        public string? Customer_Name { get; set; } = string.Empty;
        public string? Customer_Email { get; set; }
        public string? Customer_Password { get; set; }
    }
}
