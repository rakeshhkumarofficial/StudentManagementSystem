using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using StudentManagementSystem.Models;
using System.Net;
using System.Numerics;
using System.Security.Claims;
using System.Xml.Linq;

namespace StudentManagementSystem.Service
{
    public class StudentService : IStudentService
    {
        public void AddStudents(RegisterStudent Stu,string _jsonPath)
        {
            var Student = new Student()
            {
                Name = Stu.Name,
                Class = Stu.Class,
                Address = Stu.Address,
                Phone = Stu.Phone,
            };
            string json = System.IO.File.ReadAllText(_jsonPath);
            var Result = JsonConvert.DeserializeObject<List<WrapperModel>>(json);
            var Stu1 = Result[0].Stu;
            Stu1.Add(Student);

            string newJson = JsonConvert.SerializeObject(Result, Formatting.Indented);
            System.IO.File.WriteAllText(_jsonPath, newJson);
        }
        public void DeleteStudents(Guid Id,string _jsonPath)
        {
            string json = System.IO.File.ReadAllText(_jsonPath);
            var Result = JsonConvert.DeserializeObject<List<WrapperModel>>(json);
            var obj = Result[0].Stu.Find(x => x.Id == Id);
            obj.IsDelete = true;

            string newJson = JsonConvert.SerializeObject(Result, Formatting.Indented);
            System.IO.File.WriteAllText(_jsonPath, newJson);
        }
        public object GetStudents(Guid Id, string? Name, long Phone, string? Class, string? Address, int sort, int pageNumber, int records, string _jsonPath)
        {
            var json = System.IO.File.ReadAllText(_jsonPath);
            var Result = JsonConvert.DeserializeObject<List<WrapperModel>>(json);
            var query = Result[0].Stu.AsQueryable();
            var query2 = from s in query where s.IsDelete== false select s;
            if (Id == Guid.Empty && Name == null && Phone == 0 && Class == null && Address == null)
            {
                if (sort == -1)
                {
                    var sortedobj = from s in Result[0].Stu where s.IsDelete == false orderby s.Name descending select s;
                    return sortedobj;
                }
                if (sort == 1)
                { 
                    var sortedobj = from s in Result[0].Stu where s.IsDelete == false orderby s.Name select s;
                    return sortedobj;
                }
                if(pageNumber!=0 && records!=0) {
                    var pageRecords = (query2.Skip((pageNumber - 1) * records).Take(records));
                    if (pageRecords != null)
                    {
                        return pageRecords;
                    }
                }
                return query2;
            }

            var obj = from s in Result[0].Stu where s.IsDelete == false && (s.Id == Id || Id == Guid.Empty) && (s.Name == Name || Name == null) && (s.Phone == Phone || Phone == 0) && (s.Class == Class || Class==null ) && ( s.Address == Address || Address==null) select s;
           
            return obj;
            
           
        }
        public void UpdateStudents(Guid Id, UpdateStudent Stu, string _jsonPath)
        {
            string json = System.IO.File.ReadAllText(_jsonPath);
            var Result = JsonConvert.DeserializeObject<List<WrapperModel>>(json);
            var obj = Result[0].Stu.Find(x => x.Id == Id);

            if(Stu.Name!= "string") { obj.Name = Stu.Name; }
            if(Stu.Phone!=0) { obj.Phone = Stu.Phone; }
            if(Stu.Class!="string") { obj.Class = Stu.Class; }
            if(Stu.Address!="string") { obj.Address = Stu.Address; }

            string newJson = JsonConvert.SerializeObject(Result, Formatting.Indented);
            System.IO.File.WriteAllText(_jsonPath, newJson);

        }
        public void UploadProfileImage(FileUpload upload, Guid Id, string _jsonPath)
        {
            string json = System.IO.File.ReadAllText(_jsonPath);
            var Result = JsonConvert.DeserializeObject<List<WrapperModel>>(json);
            var obj = Result[0].Stu.Find(x => x.Id == Id);
            
            string folder = "wwwroot/assets/StudentImages/";
            folder += upload.ProfileImage.FileName;
            obj.ProfileImage = folder;
            string path = folder;
            upload.ProfileImage.CopyTo(new FileStream(path, FileMode.Create));
            string newJson = JsonConvert.SerializeObject(Result, Formatting.Indented);
            System.IO.File.WriteAllText(_jsonPath, newJson);
        }
    }
}
