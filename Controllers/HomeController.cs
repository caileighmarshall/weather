using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using weather.Models;
using Newtonsoft.Json.Linq;

namespace weather.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Point(string latitude, string longitude)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string uri = "https://api.weather.gov/points/" + latitude + "%2C" + longitude;
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.100 Safari/537.36");

                var responseBody = await client.GetStringAsync(uri);
                string checkResult = responseBody.ToString();
                var jo = JObject.Parse(checkResult);

                if (latitude != "" && longitude != "")
                {
                    PointModel model = new PointModel();
                    model.latitude = latitude;
                    model.longitude = longitude;
                    model.gridId = jo["properties"]["gridId"].ToString();
                    model.gridX = jo["properties"]["gridX"].ToString();
                    model.gridY = jo["properties"]["gridY"].ToString();
                    model.forecastHourlyUrl = jo["properties"]["forecastHourly"].ToString();
                    model.forecastUrl = jo["properties"]["forecast"].ToString();

                    return View(model);
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("\nException Caught!");
                Debug.WriteLine("Message :{0} ", e.Message);
            }

            return RedirectToAction("Index", "Home");
        }*/

        [HttpGet]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Weather(string latitude, string longitude)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string uri = "https://api.weather.gov/points/" + latitude + "%2C" + longitude;
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.100 Safari/537.36");

                var responseBodyPM = await client.GetStringAsync(uri);
                string checkResultPM = responseBodyPM.ToString();
                var jo = JObject.Parse(checkResultPM);

                string url = jo["properties"]["forecast"].ToString();
                var responseBody = await client.GetStringAsync(url);
                string checkResult = responseBody.ToString();
                //var jo = JObject.Parse(checkResult);

                WeatherModel model = new WeatherModel();
                model.forecastJson = checkResult;

                return View(model);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("\nException Caught!");
                Debug.WriteLine("Message :{0} ", e.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
