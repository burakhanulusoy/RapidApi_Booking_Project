using Microsoft.AspNetCore.Mvc;
using MyAcademy_RapidApi_Booking.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MyAcademy_RapidApi_Booking.Controllers
{
    public class ExchangeController : Controller
    {
        public async Task<IActionResult> Index()
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com15.p.rapidapi.com/api/v1/meta/getExchangeRates?base_currency=TRY"),
                Headers =
    {
        { "x-rapidapi-key", "7fdc49d2efmshc7819949be8e35ep10538fjsn9132de7d9d84" },
        { "x-rapidapi-host", "booking-com15.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<ExchangeViewModel>(body);
                return View(values.data.exchange_rates.ToList());
            }
            return View();




        }
    }
}
