using StudentManagementSystem.Models;

namespace StudentManagementSystem.Service
{
    public interface ITeacherService
    {
        public object GetTeachers(Guid Id, string? Name, long Phone, string? Class, string? Address, int sort, int pageNumber, int records, string _jsonPath);
        public void AddTeachers(RegisterTeacher Teach, string _jsonPath);
        public void DeleteTeachers(Guid Id, string _jsonPath);
        public void UpdateTeachers(Guid Id, UpdateTeacher Teach, string _jsonPath);
        public void UploadProfileImage(FileUpload upload, Guid Id, string _jsonPath);
        public string Login(string Username, string Password, string _jsonPath, IConfiguration _configuration);


    }
}
