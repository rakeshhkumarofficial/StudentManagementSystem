using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class RegisterTeacher
    {
        [Required(ErrorMessage = "Name is Required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Class is Required")]
        public string? Class { get; set; }
        public string? Address { get; set; }
        public long Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid email address.")]
        public string Email { get; set; }
    }
}
