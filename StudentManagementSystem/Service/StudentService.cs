using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using StudentManagementSystem.Models;
using System.Drawing;
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
            Response res = new Response();
            res.StatusCode= 200;
            res.Message = "Students details";
            if (Id == Guid.Empty && Name == null && Phone == 0 && Class == null && Address == null)
            {
                if (sort == -1)
                {
                    var sortedobj = from s in Result[0].Stu where s.IsDelete == false orderby s.Name descending select s;
                    res.Data= sortedobj;
                    return res;
                }
                if (sort == 1)
                { 
                    var sortedobj = from s in Result[0].Stu where s.IsDelete == false orderby s.Name select s;
                    res.Data = sortedobj;
                    return res;
                }
                if(pageNumber!=0 && records!=0) {
                    var pageRecords = (query2.Skip((pageNumber - 1) * records).Take(records));
                    res.Data= pageRecords;
                    if (pageRecords != null)
                    {
                        return res;
                    }
                }
                res.Data = query2;
                return res;
            }

            var obj = from s in Result[0].Stu where s.IsDelete == false && (s.Id == Id || Id == Guid.Empty) && (s.Name == Name || Name == null) && (s.Phone == Phone || Phone == 0) && (s.Class == Class || Class==null ) && ( s.Address == Address || Address==null) select s;
            res.Data = obj;
            int len = obj.Count();
            if (len == 0)
            {
                res.StatusCode = 404;
                res.Message = "Not Found";
            }
            return res;
            
           
        }
        public object UpdateStudents(Guid Id, UpdateStudent Stu, string _jsonPath)
        {
            string json = System.IO.File.ReadAllText(_jsonPath);
            var Result = JsonConvert.DeserializeObject<List<WrapperModel>>(json);
            var obj = Result[0].Stu.Find(x => x.Id == Id);
            int len = obj==null ? 0 : 1;
            if (len == 0)
            {
                Response res = new Response();
                res.StatusCode=404;
                res.Message = "Not Found";
                res.Data = obj;
                return res;
            }
            if (Stu.Name!= "string") { obj.Name = Stu.Name; }
            if(Stu.Phone!=0) { obj.Phone = Stu.Phone; }
            if(Stu.Class!="string") { obj.Class = Stu.Class; }
            if(Stu.Address!="string") { obj.Address = Stu.Address; }

            string newJson = JsonConvert.SerializeObject(Result, Formatting.Indented);
            System.IO.File.WriteAllText(_jsonPath, newJson);

           
            if(len==1)
            {
                Response res = new Response();
                res.Data = obj;
                res.StatusCode = 200;
                res.Message = "student details updated";
                return res;
            }
            return "Student Updated";

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
