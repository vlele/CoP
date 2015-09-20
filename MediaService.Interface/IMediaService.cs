using Microsoft.ServiceFabric.Services;
using System.Threading.Tasks;

namespace MediaService.Interfaces
{
    public interface IMediaService : IService
    {
        Task<byte[]> GetImageAsByteArray(string handle);
    }
}
