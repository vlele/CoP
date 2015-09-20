using CourseService.Interfaces;
using Microsoft.ServiceFabric.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CoP.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CourseController : ApiController
    {
        public IEnumerable<string> Get(string courseName)
        {
            var courseService = ServiceProxy.Create<ICourseService>(new Uri("fabric:/CoPSample/CourseService"));
            return courseService.GetCourses(courseName).Result;
        }
    }
}
