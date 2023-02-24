using Newtonsoft.Json;
using StudentManagementSystem.Models;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace StudentManagementSystem.Service
{
    public class TeacherService : ITeacherService
    {
        public void AddTeachers(RegisterTeacher Teach, string _jsonPath)
        {
          
            CreatePasswordHash(Teach.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
            var Teacher = new Teacher()
            {
                Name = Teach.Name,
                Class = Teach.Class,
                Address = Teach.Address,
                Phone = Teach.Phone,
                Username = Teach.Username,
                PasswordHash = PasswordHash,
                Passwordsalt = PasswordSalt,
                Email = Teach.Email
            };
            string json = System.IO.File.ReadAllText(_jsonPath);
            var Result = JsonConvert.DeserializeObject<List<WrapperModel>>(json);
            var Teach1 = Result[0].Teach;
            Teach1.Add(Teacher);
            string newJson = JsonConvert.SerializeObject(Result, Formatting.Indented);
            System.IO.File.WriteAllText(_jsonPath, newJson);
        }
        private void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));  
            }
        }
        public void DeleteTeachers(Guid Id, string _jsonPath)
        {
            string json = System.IO.File.ReadAllText(_jsonPath);
            var Result = JsonConvert.DeserializeObject<List<WrapperModel>>(json);
            var obj = Result[0].Teach.Find(x => x.Id == Id);
            obj.IsDelete = true;
            string newJson = JsonConvert.SerializeObject(Result, Formatting.Indented);
            System.IO.File.WriteAllText(_jsonPath, newJson);
        }
        public object GetTeachers(Guid Id, string? Name, long Phone, string? Class, string? Address, int sort, int pageNumber, int records, string _jsonPath)
        {
            var json = System.IO.File.ReadAllText(_jsonPath);
            var Result = JsonConvert.DeserializeObject<List<WrapperModel>>(json);
            var query = Result[0].Teach.AsQueryable();
            var query2 = from s in query where s.IsDelete == false select s;

            if (Id == Guid.Empty && Name == null && Phone == 0 && Class == null && Address == null)
            {
                if (sort == -1)
                {
                    var sortedobj = from s in Result[0].Teach where s.IsDelete == false orderby s.Name descending select s;
                    return sortedobj;
                }
                if (sort == 1)
                {
                    var sortedobj = from s in Result[0].Teach where s.IsDelete == false orderby s.Name select s;
                    return sortedobj;
                }
                if (pageNumber != 0 && records != 0)
                {
                    var pageRecords = (query2.Skip((pageNumber - 1) * records).Take(records));
                    if (pageRecords != null)
                    {
                        return pageRecords;
                    }
                }
                return query2;
            }

            var obj = from s in Result[0].Teach where s.IsDelete == false && (s.Id == Id || Id == Guid.Empty) && (s.Name == Name || Name == null) && (s.Phone == Phone || Phone == 0) && (s.Class == Class || Class == null) && (s.Address == Address || Address == null) select s;

            return obj;

        }
        public void UpdateTeachers(Guid Id, UpdateTeacher Teach, string _jsonPath)
        {
            string json = System.IO.File.ReadAllText(_jsonPath);
            var Result = JsonConvert.DeserializeObject<List<WrapperModel>>(json);
            var obj = Result[0].Teach.Find(x => x.Id == Id);
         
            if (Teach.Name != "string") { obj.Name = Teach.Name; }
            if (Teach.Phone != 0) { obj.Phone = Teach.Phone; }
            if (Teach.Class != "string") { obj.Class = Teach.Class; }
            if (Teach.Address != "string") { obj.Address = Teach.Address; }

            string newJson = JsonConvert.SerializeObject(Result, Formatting.Indented);
            System.IO.File.WriteAllText(_jsonPath, newJson);
        }
        public void UploadProfileImage( FileUpload upload, Guid Id, string _jsonPath)
        {
            string json = System.IO.File.ReadAllText(_jsonPath);
            var Result = JsonConvert.DeserializeObject<List<WrapperModel>>(json);
            var obj = Result[0].Teach.Find(x => x.Id == Id);
            string folder = "wwwroot/assets/TeacherImages/";
            folder += upload.ProfileImage.FileName;
            obj.ProfileImage = folder;
            string path = folder;
            upload.ProfileImage.CopyTo(new FileStream(path,FileMode.Create));
            string newJson = JsonConvert.SerializeObject(Result, Formatting.Indented);
            System.IO.File.WriteAllText(_jsonPath, newJson);
        }
    }
}
