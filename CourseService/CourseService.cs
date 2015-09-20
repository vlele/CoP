using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Services;
using CourseService.Interfaces;

namespace CourseService
{
    public class CourseService : StatelessService, ICourseService
    {
        protected override ICommunicationListener CreateCommunicationListener()
        {
            return new ServiceCommunicationListener<ICourseService>(this);
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            int iterations = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                ServiceEventSource.Current.ServiceMessage(this, "Working-{0}", iterations++);
                await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
            }
        }

        public Task<IEnumerable<string>> GetCourses(string courseName)
        {
            return Task.FromResult(new MLProxy().GetCourses(courseName));
        }
        
    }
}
