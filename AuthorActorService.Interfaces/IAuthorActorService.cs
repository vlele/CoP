using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using AuthorService.Interfaces;

namespace AuthorActorService.Interfaces
{
    public interface IAuthorActorService : IActor
    {
        Task<AuthorModel> GetAuthor();

        Task SaveAuthor(AuthorModel author);
    }
}
