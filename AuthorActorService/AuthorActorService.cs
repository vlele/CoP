using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthorActorService.Interfaces;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Actors;
using AuthorService.Interfaces;

namespace AuthorActorService
{
    public class AuthorActorService : Actor<AuthorActorServiceState>, IAuthorActorService
    {
        public override Task OnActivateAsync()
        {
            if (this.State == null)
            {
                this.State = new AuthorActorServiceState();
            }

            ActorEventSource.Current.ActorMessage(this, "State initialized.");
            return Task.FromResult(true);
        }

        public Task<AuthorModel> GetAuthor()
        {
            return Task.FromResult(new AuthorModel()
            {
                Firstname = this.State.Firstname,
                Lastname = this.State.Lastname,
                Twitter = this.State.Twitter,
                Phone = this.State.Phone,
                Email = this.State.Email
            });
        }

        public Task SaveAuthor(AuthorModel author)
        {
            this.State.Firstname = author.Firstname;
            this.State.Lastname = author.Lastname;
            this.State.Twitter = author.Twitter;
            this.State.Phone = author.Phone;
            this.State.Email = author.Email;

            return Task.FromResult(true);
        }
    }
}
