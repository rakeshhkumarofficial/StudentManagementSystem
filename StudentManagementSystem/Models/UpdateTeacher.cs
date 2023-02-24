using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class UpdateTeacher
    {
        [Required(ErrorMessage = "Name is Required")]
        public string? Name { get; set; } = "string";
       
        [Required(ErrorMessage = "Class is Required")]
        public string? Class { get; set; } = "string";
        public string? Address { get; set; } = "string";
        public long Phone { get; set; } = 0;
    }
}
