using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StudentManagementSystem.Models;
using StudentManagementSystem.Service;
using System.Net;
using System.Numerics;
using System.Security.Claims;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authorization;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly string _jsonPath = "C:\\Users\\ChicMic Technologies\\Desktop\\SMS\\file.json";

        [HttpGet]
        public IActionResult GetStu(Guid Id, string? Name, long Phone, string? Class, string? Address, int sort, int pageNumber, int records)
        {
            IStudentService service = new StudentService();
            var res = service.GetStudents(Id, Name, Phone, Class, Address, sort,pageNumber,records,_jsonPath);
            return Ok(res);
        }

        [HttpPost]
        public IActionResult PostStu(RegisterStudent Stu)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            IStudentService service = new StudentService();
            service.AddStudents(Stu, _jsonPath);
            return Ok("New Student Added");
        }

        [HttpDelete]
        public IActionResult DeleteStu(Guid Id) { 
            IStudentService service = new StudentService();
            service.DeleteStudents(Id,_jsonPath);
            return Ok("Student is deleted");
        }

        [HttpPut]
        public IActionResult PutStu(Guid Id, UpdateStudent Stu)
        {
            IStudentService service = new StudentService();
            service.UpdateStudents(Id, Stu, _jsonPath);
            return Ok("Student Details updated");
        }

       
    }
}
