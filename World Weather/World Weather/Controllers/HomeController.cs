using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using World_Weather.Models;
using Microsoft.Identity.Client;

namespace World_Weather.Controllers
{
    public class HomeController : Controller
    {
        private async Task<string> GetAccessTokenAsync()
        {
            string clientId = "YOUR_CLIENT_ID";
            string clientSecret = "YOUR_CLIENT_SECRET";
            string tenantId = "YOUR_TENANT_ID";
            string scope = "https://graph.microsoft.com/.default";
            string authority = $"https://login.microsoftonline.com/{tenantId}";

            var app = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithClientSecret(clientSecret)
                .WithAuthority(authority)
                .Build();

            var authResult = await app.AcquireTokenForClient(new string[] { scope }).ExecuteAsync();
            return authResult.AccessToken;
        }


        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();

            // OneDrive URL der JSON-Datei
            string jsonFileUrl = "https://fhtw-my.sharepoint.com/:u:/g/personal/wi22m002_technikum-wien_at/Edob0oumMUxAoUHnVGqwT-MBLiBrSGd27GCeKlH47iX6pw?e=Zvv9qT";

            string accessToken = await GetAccessTokenAsync();

            // Set authorization header with the access token
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            // Download the JSON file content
            var jsonContent = await httpClient.GetStringAsync(jsonFileUrl);

            // Deserialize the JSON into the DashboardModel
            var dashboardModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DashboardModel>(jsonContent);

            return View(dashboardModel);
        }
    }
}
