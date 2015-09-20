using AuthorService.Interfaces;
using Microsoft.ServiceFabric.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CoP.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthorController : ApiController
    {
        public IEnumerable<AuthorModel> Get(string firstname, string lastname)
        {
            string authorSvcPName = "999977808";
            var authorService = ServiceProxy.Create<IAuthorService>(authorSvcPName, new Uri("fabric:/CoPSample/AuthorService"));
            return authorService.GetAuthors(firstname, lastname).Result;
        }
    }
}
