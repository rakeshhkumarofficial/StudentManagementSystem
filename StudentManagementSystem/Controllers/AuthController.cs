using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StudentManagementSystem.Models;
using StudentManagementSystem.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _jsonPath = "C:\\Users\\ChicMic Technologies\\Desktop\\SMS\\StudentManagementSystem\\StudentManagementSystem\\file.json";
        public readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult LoginTeacher(string Username, string Password)
        {
            ITeacherService service = new TeacherService();
            string token = service.Login(Username, Password,_jsonPath, _configuration);
            return Ok(token);
        }
       
    }
}
