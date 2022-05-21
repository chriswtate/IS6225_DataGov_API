using IS6225_DataGov_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IS6225_DataGov_API.Controllers
{
    public class HomeController : Controller
    {
        HttpClient httpClient;

        static string BASE_URL = "https://developer.nps.gov/api/v1";
        static string API_KEY = "ZUs1IPsdn48NCsa7rteF91RCBhcUDahoQSohL5Hg"; //Add your API key here inside ""
        public IActionResult Index()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string NATIONAL_PARK_API_PATH = BASE_URL + "/parks?limit=20";
            string parksData = "";

            Parks parks = null;

            httpClient.BaseAddress = new Uri(NATIONAL_PARK_API_PATH);
            //httpClient.BaseAddress = new Uri(BASE_URL);

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(NATIONAL_PARK_API_PATH)
                                                        .GetAwaiter().GetResult();
                //HttpResponseMessage response = httpClient.GetAsync(BASE_URL)
                //                                        .GetAwaiter().GetResult();



                if (response.IsSuccessStatusCode)
                {
                    parksData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!parksData.Equals(""))
                {
                    // JsonConvert is part of the NewtonSoft.Json Nuget package
                    parks = JsonConvert.DeserializeObject<Parks>(parksData);
                }

                //dbContext.Parks.Add(parks);
                //await dbContext.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }

            return View(parks);
        }
    }
}