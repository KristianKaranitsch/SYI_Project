using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using World_Weather.Models;

namespace World_Weather.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetCityData(CityModel model)
        {
            if (!ModelState.IsValid)
            {
                // Wenn die Validierung fehlschlägt, kehren Sie zur View zurück und zeigen Sie Fehlermeldungen an
                return View("Index", model);
            }

            // Hier implementieren Sie den Code, um die Longitude und Latitude für den ausgewählten Stadtname aus der Datenbank abzurufen
            // Verwenden Sie dazu die Stadtname-Parameter und führen Sie die Datenbankabfrage aus
            string connectionString = "YourConnectionString"; // Verbindungszeichenfolge zur Datenbank
            string query = $"SELECT Longitude, Latitude FROM cities WHERE Cityname = '{model.SelectedCity}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model.Longitude = reader["Longitude"].ToString();
                            model.Latitude = reader["Latitude"].ToString();
                        }
                        else
                        {
                            // Falls keine Übereinstimmung für den ausgewählten Stadtname gefunden wurde, können Sie Fehlerbehandlung durchführen
                            ModelState.AddModelError(string.Empty, "Die ausgewählte Stadt wurde nicht gefunden.");
                            return View("Index", model);
                        }
                    }
                }
            }

            return RedirectToAction("GetForecast", model);
        }

        public IActionResult GetForecast(CityModel model)
        {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={model.Latitude}&longitude={model.Longitude}&hourly=temperature_2m";

            // Hier können Sie den URL weiterverarbeiten, beispielsweise die API aufrufen und die Daten anzeigen

            return View(model);
        }

        public IActionResult GetWeatherData(CityModel model)
        {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={model.Latitude}&longitude={model.Longitude}&hourly=temperature_2m";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    dynamic jsonData = JsonConvert.DeserializeObject(content);

                    WeatherDataModel weatherData = new WeatherDataModel();
                    weatherData.Temperature = jsonData.hourly.temperature_2m.value.ToString();
                    // Weitere Zuweisungen für die restlichen Eigenschaften des Models

                    return View(weatherData); // Das Model an die View übergeben
                }
                else
                {
                    // Fehlerbehandlung, wenn die Anfrage fehlschlägt
                    string errorMessage = "Fehler beim Abrufen der Wetterdaten.";
                    return View("Error", errorMessage); // Fehlermeldung an die Error-View übergeben
                }
            }

            // ...
        }
    }
}
