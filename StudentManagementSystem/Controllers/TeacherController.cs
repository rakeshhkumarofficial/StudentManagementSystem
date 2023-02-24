using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Service;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly string _jsonPath = "C:\\Users\\ChicMic Technologies\\Desktop\\SMS\\file.json";

        [HttpGet,Authorize]
        public IActionResult GetTeach(Guid Id, string? Name, long Phone, string? Class, string? Address, int sort, int pageNumber, int records)
        {
            ITeacherService service = new TeacherService();
            var res = service.GetTeachers(Id, Name, Phone, Class, Address, sort, pageNumber, records, _jsonPath);
            return Ok(res);
        }

        [HttpPost]  
        public IActionResult PostTeach( RegisterTeacher Teach)
        {
            ITeacherService service = new TeacherService();
            service.AddTeachers(Teach, _jsonPath);
            return Ok("New Teacher Added");
        }

        [HttpDelete, Authorize]  
        public IActionResult DeleteStu(Guid Id)
        {
            ITeacherService service = new TeacherService();
            service.DeleteTeachers(Id, _jsonPath);
            return Ok("Teacher is deleted");
        }

        [HttpPut, Authorize]  
        public IActionResult PutTeach(Guid Id, UpdateTeacher Teach)
        {
            ITeacherService service = new TeacherService();
            service.UpdateTeachers(Id, Teach, _jsonPath);
            return Ok("Teacher Details upadted");
        }
    }
}
