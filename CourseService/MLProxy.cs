using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Helpers;

namespace CourseService
{
    public class MLProxy
    {
        private const string serviceUrl = "";
        private const string apiKey = ""; 

        public MLProxy()
        {
        }

        public IEnumerable<string> GetCourses(string courseName)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, StringTable>() 
                    { 
                        { 
                            "input1", 
                            new StringTable() 
                            {
                                ColumnNames = new string[] {"CourseName"},
                                Values = new string[,] {  { courseName } }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                
                client.BaseAddress = new Uri(serviceUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                List<string> courses = new List<string>();

                try
                {
                    HttpResponseMessage response = client.PostAsJsonAsync("", scoreRequest).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = response.Content.ReadAsStringAsync().Result;
                        dynamic courseResult = Json.Decode(jsonString);
                        foreach (var course in courseResult.Results.output2.value.Values[0])
                        {
                            courses.Add(Convert.ToString(course));
                        }
                    }
                }
                catch (Exception ex)
                {
                    courses.Add(ex.Message);
                    courses.Add(ex.StackTrace);
                }

                return courses;
            }
        }

        class StringTable
        {
            public string[] ColumnNames { get; set; }

            public string[,] Values { get; set; }
        }
    }

}
