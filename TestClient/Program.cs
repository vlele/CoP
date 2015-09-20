using AuthorActorService.Interfaces;
using AuthorService.Interfaces;
using CourseService.Interfaces;
using EmailActorService.Interfaces;
using MediaService.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Services;
using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;

namespace MediaService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region CoursesService

            //var courseService = ServiceProxy.Create<ICourseService>(new Uri("fabric:/CoPSample/CourseService"));

            //Console.WriteLine("Getting courses for keyword 'visual' ...");

            //var courses = courseService.GetCourses("visual").Result;
            //foreach (var course in courses)
            //{
            //    Console.WriteLine(course);
            //}

            #endregion

            #region EmailActorService

            //IEmailActorService emailActorService = ActorProxy.Create<IEmailActorService>(ActorId.NewId(), "fabric:/CoPSample");
            //string emailId = "sajad.deyargaroo@appliedis.com";

            //Console.WriteLine(String.Format("Sending email to {0} ...", emailId));
            //bool result  = emailActorService.SendEmail(emailId, "Hi from Service Fabric", "sajad_").Result;
            //Console.WriteLine(result ? "Email Sent Successfully." : "Could not send email, please try again later.");

            //Console.WriteLine("Getting Email History ...");

            //var emails = emailActorService.EmailHistory("01/01/2015", "12/12/2015").Result;
            //Console.WriteLine("{0}\t{1}\t{2}\t{3}", "DateTime", "Status", "To", "Message"); ;

            //foreach (var email in emails)
            //{
            //    Console.WriteLine("{0}\t{1}\t{2}\t{3}", email.RequestDateTime.ToString(), email.Status, email.To, email.Message);
            //}

            #endregion

            #region MediaService

            //string handle = "sajad_";
            //string filePath = @"C:\Tmp\Images\Dld.jpg";
            //var mediaService = ServiceProxy.Create<IMediaService>(new Uri("fabric:/CoPSample/MediaService"));

            //Console.WriteLine("Dowloading image of handle '{0}' ...", handle);

            //var fileBytes = mediaService.GetImageAsByteArray(handle).Result;
            //System.IO.File.WriteAllBytes(filePath, fileBytes);

            //Console.WriteLine("File downloaded successfully at '{0}'", filePath);

            #endregion

            #region AuthorActorService

            //IAuthorActorService authorActorService = ActorProxy.Create<IAuthorActorService>(new ActorId("sajad_"), "fabric:/CoPSample");
            //AuthorModel authorActor = new AuthorModel()
            //{
            //    Firstname = "Scott",
            //    Lastname = "Allen",
            //    Twitter = "OdeToCode",
            //    Phone = "8780000888",
            //    Email = "Scott.Allen@testmail.com"
            //};

            //Console.WriteLine("Saving author {0} {1} ...", authorActor.Firstname, authorActor.Lastname);
            //authorActorService.SaveAuthor(authorActor);
            //Console.WriteLine("Author saved successfully.");
            //Console.WriteLine("Fetching author details ...");
            //AuthorModel authorActorRet = authorActorService.GetAuthor().Result;
            //Console.WriteLine("Firstname: {0}", authorActorRet.Firstname);
            //Console.WriteLine("Lastname: {0}", authorActorRet.Lastname);
            //Console.WriteLine("Twitter: {0}", authorActorRet.Twitter);
            //Console.WriteLine("Phone: {0}", authorActorRet.Phone);
            //Console.WriteLine("Email: {0}", authorActorRet.Email);

            #endregion

            #region AuthorService

            //string authorSvcPName = "999977808";
            //var authorService = ServiceProxy.Create<IAuthorService>(authorSvcPName, new Uri("fabric:/CoPSample/AuthorService"));

            //Console.WriteLine("Getting Authors ...");

            //var authors = authorService.GetAuthors(string.Empty, string.Empty).Result;
            //Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", "Firstname", "Lastname", "Twitter", "Phone", "Email"); ;

            //foreach (var author in authors)
            //{
            //    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", author.Firstname, author.Lastname, author.Twitter, author.Phone, author.Email);
            //}

            #endregion

            Console.ReadLine();

        }
    }
}
