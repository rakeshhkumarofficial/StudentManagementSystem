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

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid email address.")]
        public string? Email { get; set; } = "user@example.com";

    }
}
