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
        public IActionResult Index()
        {
            //Befüllen CityModel
            List<CityModel> cities = getCities();

            //Dashboard
            DashboardModel model = GetJsonFile();

            //Suche
            DropdownGenerator(model,cities);

            return View(model);
        }

        [HttpGet]
        public IActionResult GetCityCoordinates(string city)
        {
            List<CityModel> cities = getCities();
            // Suchen Sie die Stadt in der Liste cities und geben Sie die entsprechenden Längen- und Breitengrade zurück
            var cityData = cities.FirstOrDefault(c => c.CapitalName == city);

            if (cityData != null)
            {
                var coordinates = new CityModel
                {
                    CapitalLongitude = cityData.CapitalLongitude,
                    CapitalLatitude = cityData.CapitalLatitude
                };

                return Json(coordinates);
            }

            return NotFound();
        }

        private List<CityModel> getCities()
        {
            string jsonFilePath = "\\Users\\Kristian\\OneDrive - FH Technikum Wien\\Dokumente\\MWI\\SystemIntegration\\SYI_Project\\World Weather\\World Weather\\Models\\cities.json";
            // Lesen Sie die JSON-Datei und deserialisieren Sie sie in eine Liste von CityModel-Objekten
            List<CityModel> cities = JsonConvert.DeserializeObject<List<CityModel>>(System.IO.File.ReadAllText(jsonFilePath));

            return cities;
        }

        public void DropdownGenerator(DashboardModel model, List<CityModel>cities)
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
            try{
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
        //"C:\Users\Kristian\OneDrive - FH Technikum Wien\Systemintegration\SYI_Project\Dashboard_log\Dashboard_20230528_1755.json"


    }
}
