using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Name is Required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Class is Required")]       
        public string? Class { get; set; }
        public string? Address { get; set; }
        public long Phone { get; set; }
        public string? ProfileImage { get; set; } = "string";
        public bool? IsDelete { get; set; }=false;
    }
}
