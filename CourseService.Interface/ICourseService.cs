using Microsoft.ServiceFabric.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseService.Interfaces
{
    public interface ICourseService : IService
    {
        Task<IEnumerable<string>> GetCourses(string courseName);
    }
}
