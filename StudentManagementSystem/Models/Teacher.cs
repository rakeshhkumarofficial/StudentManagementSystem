using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Teacher
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Name is Required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Class is Required")]
        public string? Class { get; set; }
        public string? Address { get; set; }
        public long Phone { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; } 
        public byte[] Passwordsalt { get; set; } 

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid email address.")]
        public string Email { get; set; }
        public string? ProfileImage { get; set; } = "string";
        public bool? IsDelete { get; set; } = false;
    }
}
