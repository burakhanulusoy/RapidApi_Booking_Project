using Microsoft.AspNetCore.Mvc;
using MyAcademy_RapidApi_Booking.Models;
using Newtonsoft.Json;

namespace MyAcademy_RapidApi_Booking.Controllers
{
    public class MovieController : Controller
    {
        public async Task<IActionResult> ImdbTop100List()
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                Headers =
    {
        { "x-rapidapi-key", "7fdc49d2efmshc7819949be8e35ep10538fjsn9132de7d9d84" },
        { "x-rapidapi-host", "imdb-top-100-movies.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ImdbTop100ViewModel>>(body);
                return View(values.ToList());
            }



        }
    }
}
