using EmailActorService.Interfaces;
using EmailActorService.Interfaces.Models;
using Microsoft.ServiceFabric.Actors;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CoP.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmailController : ApiController
    {
        public bool Get(string to, string message, string thandle)
        {
            IEmailActorService emailActorService = ActorProxy.Create<IEmailActorService>(ActorId.NewId(), "fabric:/CoPSample");
            return emailActorService.SendEmail(to, message, thandle).Result;
        }

        public IEnumerable<EmailModel> Get(string fromDate, string toDate)
        {
            IEmailActorService emailActorService = ActorProxy.Create<IEmailActorService>(ActorId.NewId(), "fabric:/CoPSample");
            return emailActorService.EmailHistory(fromDate, toDate).Result;
        }
    }
}
