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

namespace World_Weather.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //Dashboard
            DashboardModel model = GetJsonFile();

           

            //Suche
            

            return View(model);
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
