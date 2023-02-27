using StudentManagementSystem.Models;

namespace StudentManagementSystem.Service
{
    public interface IStudentService
    {
        public object GetStudents( Guid Id, string? Name, long Phone, string? Class, string? Address,int sort, int pageNumber, int records, string _jsonPath);
        public void AddStudents(RegisterStudent Stu,string _jsonPath);
        public void DeleteStudents(Guid id,string _jsonPath);
        public object UpdateStudents(Guid Id, UpdateStudent Stu, string _jsonPath);
        public void UploadProfileImage(FileUpload upload, Guid Id, string _jsonPath);
    }
}
