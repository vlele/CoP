using MediaService.Interfaces;
using Microsoft.ServiceFabric.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediaService
{
    public class MediaService : StatelessService, IMediaService
    {
        public Task<byte[]> GetImageAsByteArray(string handle)
        {
            return Task.FromResult(MediaDataService.GetImageAsByteArray(handle));
        }

        protected override ICommunicationListener CreateCommunicationListener()
        {
            return new ServiceCommunicationListener<IMediaService>(this);
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
    }
}
