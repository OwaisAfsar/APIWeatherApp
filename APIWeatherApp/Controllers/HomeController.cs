using APIWeatherApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using Nancy.Json;

namespace APIWeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //string baseURL = "https://api.openweathermap.org/data/2.5/weather?q=Munich&APPID=3b89076394786db067776f1f6da11b1f";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public async Task<ActionResult> Index(string cityName)
        {
            string baseURL = "https://api.openweathermap.org/data/2.5/weather?q=" + cityName + "&APPID=3b89076394786db067776f1f6da11b1f";
            
            ResultModel resultModel = new ResultModel();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseURL);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await httpClient.GetAsync(baseURL);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var results = responseMessage.Content.ReadAsStringAsync().Result;
                    RootObject weatherInformation = (new JavaScriptSerializer()).Deserialize<RootObject>(results);

                    resultModel = WeatherData.GetWeatherData(resultModel, weatherInformation);
                }
                else
                {
                    Console.WriteLine("error");
                }
            }
            return View("Index", resultModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}