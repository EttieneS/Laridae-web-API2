using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Laeridae_API.Models;
using System.Net.Http;

namespace Laeridae_API.Controllers
{
    [EnableCors(origins: "https://localhost:44376", headers: "*", methods: "*")]
    public class StudentsController : ApiController
    {
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> StudentsList()
        {
            using (SchoolDBContext context = new SchoolDBContext())
            {
                return Json(await context.Students.ToListAsync());
            }
        }

        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateStudent(Students student)
        {
            using (var context = new SchoolDBContext())
            {
                context.Students.Add(new Students()
                {
                    Name = student.Name,
                    Date = student.Date,
                    TeacherFK = student.TeacherFK

                });

                context.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                      new { Success = true, RedirectUrl = "https://localhost:44376/Students" });
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteStudent(int id)
        {
            using (var context = new SchoolDBContext())
            {
                var student = context.Students
                    .Where(t => t.Id == id)
                    .FirstOrDefault();
                context.Students.Remove(student);
                context.SaveChanges();
            }
            return StatusCode(HttpStatusCode.OK);
        }

        [System.Web.Http.HttpPost]
        public Students GetStudent(int id)
        {
            using (var context = new SchoolDBContext())
            {
                var student = context.Students
                    .Where(t => t.Id == id)
                    .FirstOrDefault();
                return student;
            }
            return null;
        }

        [System.Web.Http.AcceptVerbs("PUT")]
        [System.Web.Http.HttpPut]
        public HttpResponseMessage EditStudent(Students student)
        {
            using (var context = new SchoolDBContext())
            {
                var originalStudent = context.Students
                   .Where(t => t.Id == student.Id)
                   .FirstOrDefault();
                originalStudent.Name = student.Name;
                originalStudent.Date = student.Date;
                originalStudent.TeacherFK = student.TeacherFK;
                
                context.SaveChanges();
            }
            
            return Request.CreateResponse(HttpStatusCode.OK,
                      new { RedirectUrl = "https://localhost:44376/Students" });
        }

        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetStudentTeacher()
        {
            using (SchoolDBContext context = new SchoolDBContext())
            {
                var StudentTeacher = (from s in context.Students
                                      join t in context.Teachers on s.TeacherFK equals t.Id
                                      select new
                                      {
                                          Id = s.Id,
                                          Name = s.Name,
                                          Date = s.Date,
                                          TeacherId = t.Id,
                                          TeacherName = t.Name
                                      });
                return Json(await StudentTeacher.ToListAsync());
            }
        }

    }
}
