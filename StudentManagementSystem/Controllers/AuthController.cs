using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StudentManagementSystem.Models;
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
        private readonly string _jsonPath = "C:\\Users\\ChicMic Technologies\\Desktop\\SMS\\file.json";
        public readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login(string Username, string Password)
        {
            string json = System.IO.File.ReadAllText(_jsonPath);
            var Result = JsonConvert.DeserializeObject<List<WrapperModel>>(json);
            var obj = Result[0].Teach.Find(x => x.Username == Username);        

            if (obj == null)
            {
                return BadRequest("User Not Found");
            }
            if (!VerifyPasswordHash(Password,obj.PasswordHash,obj.Passwordsalt))
            {
                return Ok("wrong password");
            }
            string token = CreateToken(obj);

            return Ok(token);
        }
        private string CreateToken(Teacher obj)
        {
            List<Claim> claims = new List<Claim>
           {
               new Claim(ClaimTypes.Name,obj.Username)
           };
            var Key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(Key,SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims : claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);  
            return jwt;
        }
        private bool VerifyPasswordHash(string Password,byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                return computedHash.SequenceEqual(PasswordHash);
            }
        }
    }
}
