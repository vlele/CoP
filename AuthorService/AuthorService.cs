using AuthorActorService.Interfaces;
using AuthorService.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AuthorService
{
    public class AuthorService : StatefulService, IAuthorService
    {
        private readonly string AuthorActorsDictName = "actorDictionary";

        public async Task<IEnumerable<AuthorModel>> GetAuthors(string firstname, string lastname)
        {
            IList<AuthorModel> authorList = new List<AuthorModel>();
            var actorDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, long>>(AuthorActorsDictName);
            
            if (actorDictionary.GetCountAsync().Result <= 0)
            {
                var authors = AuthorDataService.GetAuthors(String.Empty, String.Empty);

                using (var tx = this.StateManager.CreateTransaction())
                {
                    foreach (var author in authors)
                    {
                        ActorId newId = ActorId.NewId();
                        IAuthorActorService authorActorService = ActorProxy.Create<IAuthorActorService>(newId, "fabric:/CoPSample");
                        long actorLongId = authorActorService.GetActorId().GetLongId();

                        await authorActorService.SaveAuthor(new AuthorModel()
                        {
                            Firstname = author.Firstname,
                            Lastname = author.Lastname,
                            Twitter = author.RowKey,
                            Phone = author.Phone,
                            Email = author.Email
                        });

                        await actorDictionary.AddOrUpdateAsync(tx, author.RowKey, actorLongId,
                            (key, oldValue) => actorLongId);
                    }

                    await tx.CommitAsync();
                }

            }

            using (var tx = this.StateManager.CreateTransaction())
            {
                var actorIds = actorDictionary.CreateEnumerable();

                foreach (var actorId in actorIds)
                {
                    IAuthorActorService authorActorService = ActorProxy.Create<IAuthorActorService>(new ActorId(actorId.Value), "fabric:/CoPSample");
                    var author = await authorActorService.GetAuthor();
                    authorList.Add(author);
                }

                await tx.CommitAsync();
            }

            return authorList.Where(a => 
                (String.IsNullOrEmpty(firstname) ? true : a.Firstname.ToLower().Contains(firstname.ToLower())) &&
                (String.IsNullOrEmpty(lastname) ? true : a.Lastname.ToLower().Contains(lastname.ToLower())));
        }

        protected override ICommunicationListener CreateCommunicationListener()
        {
            return new ServiceCommunicationListener<IAuthorService>(this);
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
