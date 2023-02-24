using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Service;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileUploadController : ControllerBase
    {
        private readonly string _jsonPath = "C:\\Users\\ChicMic Technologies\\Desktop\\SMS\\file.json";
        
        [HttpPost]
        public IActionResult PutTeacherImage([FromForm]FileUpload upload, Guid Id ,string type)
        {
            if (type.ToUpper() == "TEACHER")
            {
                ITeacherService service = new TeacherService();
                service.UploadProfileImage(upload, Id, _jsonPath);
            }
            if (type.ToUpper() == "STUDENT")
            {
                IStudentService service = new StudentService();
                service.UploadProfileImage(upload, Id, _jsonPath);
            }
            return Ok("Image Uploaded Successfully..");
        }
        
       
    }
}
