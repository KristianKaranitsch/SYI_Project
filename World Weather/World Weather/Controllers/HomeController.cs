using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using World_Weather.Models;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace World_Weather.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly HttpClient httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient();
        }
        public IActionResult Index()
        {
            //Befüllen CityModel
            List<CityModel> cities = GetCities();

            //Dashboard
            DashboardModel model = GetJsonFile();
            //Suche
            DropdownGenerator(model, cities);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCityCoordinates(string city)
        {
            List<CityModel> cities = GetCities();
            CityModel cityData = cities.FirstOrDefault(c => c.CapitalName == city);

            if (cityData != null)
            {
                var longitude = cityData.CapitalLongitude;
                var latitude = cityData.CapitalLatitude;

                var jsonPayload = JsonConvert.SerializeObject(new
                {
                    longitude = longitude,
                    latitude = latitude
                });

                var url = "https://prod-10.northeurope.logic.azure.com/workflows/bda52adc1d9c4df5b8cc4a1bc4de1913/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=YYSjUXdYCmk0qdFazAyx2B8q5yfyPuf2Gi6ke8Wn-VI";

                using (var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json"))
                {
                    var response = await httpClient.PostAsync(url, content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                }
                using (HttpClient httpClient = new HttpClient())
                {
         
                string meteoapi = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true&timezone=Europe%2FBerlin";

                    HttpResponseMessage response = await httpClient.GetAsync(meteoapi);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
                        string temperature = responseObject.current_weather.temperature?.ToString();
                        return Json(new { CapitalTemperature = temperature });
                    }
                    else
                    {
                        // Bei einem Fehlerstatuscode behandele den Fehler hier
                    }
                }
            }

            var errorResponse = new { Error = "Stadt nicht gefunden" };
            var errorJson = JsonConvert.SerializeObject(errorResponse);
            return Content(errorJson, "application/json");
        }

        public List<CityModel> GetCities()
        {
            string jsonFilePath = "C:\\Users\\Kristian\\Documents\\MWI\\SystemIntegration\\SYI_Project\\World Weather\\World Weather\\Models\\cities.json";
            // Lesen Sie die JSON-Datei und deserialisieren Sie sie in eine Liste von CityModel-Objekten
            List<CityModel> cities = JsonConvert.DeserializeObject<List<CityModel>>(System.IO.File.ReadAllText(jsonFilePath));

            return cities;
        }

        public void DropdownGenerator(DashboardModel model, List<CityModel> cities)
        {
            // Extrahieren Sie die Hauptstädte aus der Liste
            List<string> capitalNames = cities.Select(c => c.CapitalName).ToList();

            // Erstellen Sie eine Instanz von CityDropdownModel und setzen Sie die Hauptstädte als Optionen
            var dropdownOptions = new CityDropdownModel
            {
                CityPickOptions = capitalNames
            };

            // Setzen Sie die Hauptstädte als Optionen für das Dropdown-Feld in Ihrem Model
            model.CityPickOptions = capitalNames;
        }

        public DashboardModel GetJsonFile()
        {
            try
            {
                string folderpath = "C:\\Users\\Kristian\\OneDrive - FH Technikum Wien\\Systemintegration\\SYI_Project\\Dashboard_log";
                string filepath = GetLatestJsonFile(folderpath);
                string jsoncontent = System.IO.File.ReadAllText(filepath);
                // JSON-Datei deserialisieren und in das DashboardModel konvertieren
                DashboardModel dashboardModel = JsonConvert.DeserializeObject<DashboardModel>(jsoncontent);

                return dashboardModel;
            }
            catch
            {
                DashboardModel dashboardModel = new DashboardModel();
                dashboardModel.Wien = 13;
                dashboardModel.NewYork = 23;
                dashboardModel.Sydney = 42;
                dashboardModel.Tokio = 24;
                return dashboardModel;
            }

        }
        public string GetLatestJsonFile(string folderPath)
        {
            // Überprüfen, ob der angegebene Ordner existiert
            if (!Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException($"Der Ordner {folderPath} existiert nicht.");
            }

            // Alle JSON-Dateien im Ordner abrufen
            string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");

            // Überprüfen, ob JSON-Dateien vorhanden sind
            if (jsonFiles.Length == 0)
            {
                throw new FileNotFoundException("Keine JSON-Dateien im angegebenen Ordner gefunden.");
            }

            // Das aktuellste JSON-Datei basierend auf dem Dateinamen (mit Datum und Uhrzeit) ermitteln
            string latestJsonFile = jsonFiles.OrderByDescending(f => f).First();

            return latestJsonFile;
        }
        


    }
}
