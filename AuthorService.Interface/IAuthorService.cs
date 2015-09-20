using Microsoft.ServiceFabric.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorService.Interfaces
{
    public interface IAuthorService : IService
    {
        Task<IEnumerable<AuthorModel>> GetAuthors(string firstname, string lastname);
    }
}
