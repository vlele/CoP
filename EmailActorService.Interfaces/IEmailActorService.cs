using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using EmailActorService.Interfaces.Models;

namespace EmailActorService.Interfaces
{
    public interface IEmailActorService : IActor
    {
        Task<bool> SendEmail(string to, string message, string thandle);
        Task<IEnumerable<EmailModel>> EmailHistory(string fromDate, string toDate);
    }
}
