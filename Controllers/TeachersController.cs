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
    [EnableCors( origins: "https://localhost:44376", headers: "*", methods:"*")]
    public class TeachersController : ApiController
    {
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> TeachersList()
        {
            using (SchoolDBContext context = new SchoolDBContext())
            {
                return Json(await context.Teachers.ToListAsync());
            }
        }

        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateTeacher(Teachers jsonResult)
        {
            using (var context = new SchoolDBContext())
            {
                context.Teachers.Add(new Teachers()
                {
                    Name = jsonResult.Name,
                    Date = jsonResult.Date,
                    Active = jsonResult.Active,
                    Salary = jsonResult.Salary
                });   

                context.SaveChanges();
            }
            //todo redirect or success??
            return StatusCode(HttpStatusCode.OK);
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteTeacher(int id)
        { 
            using (var context = new SchoolDBContext())
            {
                var teacher = context.Teachers
                    .Where(t => t.Id == id)
                    .FirstOrDefault();
                context.Teachers.Remove(teacher);
                context.SaveChanges();
            }
            return StatusCode(HttpStatusCode.OK);
        }

        [System.Web.Http.HttpPost]
        public Teachers GetTeacher(int id)
        {
            using (var context = new SchoolDBContext())
            {
                var teacher = context.Teachers
                    .Where(t => t.Id == id)
                    .FirstOrDefault();
                return teacher;
            }
            return null;
        }

        [System.Web.Http.AcceptVerbs("PUT")]
        [System.Web.Http.HttpPut]
        public HttpResponseMessage EditTeacher(Teachers teacher)
        {
            using (var context = new SchoolDBContext())
            {
                var originalTeacher = context.Teachers
                   .Where(t => t.Id == teacher.Id)
                   .FirstOrDefault();
                originalTeacher.Name = teacher.Name;
                originalTeacher.Date = teacher.Date;
                originalTeacher.Active = teacher.Active;
                originalTeacher.Salary = teacher.Salary;

                context.SaveChanges();
            }
            var newUrl = this.Url.Link("Default", new
            {
                Controller = "Teachers",
                Action = "Index"
            });
            return Request.CreateResponse(HttpStatusCode.OK,
                      new { Success = true, RedirectUrl = "https://localhost:44376/Teachers" });
        }

       

    }
}
