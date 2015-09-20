using MediaService.Interfaces;
using Microsoft.ServiceFabric.Services;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CoP.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MediaController : ApiController
    {
        public HttpResponseMessage Get(string handle)
        {
            var mediaService = ServiceProxy.Create<IMediaService>(new Uri("fabric:/CoPSample/MediaService"));
            var fileBytes = mediaService.GetImageAsByteArray(handle).Result;

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(new MemoryStream(fileBytes))
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return response;
        }
    }
}
