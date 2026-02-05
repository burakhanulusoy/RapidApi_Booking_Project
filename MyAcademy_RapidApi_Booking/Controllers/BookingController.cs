using Microsoft.AspNetCore.Mvc;
using MyAcademy_RapidApi_Booking.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MyAcademy_RapidApi_Booking.Controllers
{
    public class BookingController : Controller
    {
        public async Task<IActionResult> Index()
        {
            
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com15.p.rapidapi.com/api/v1/hotels/searchHotels?dest_id=-735338&search_type=city&arrival_date=2026-02-06&departure_date=2026-02-22&adults=2&children_age=0%2C17&room_qty=1&page_number=1&units=metric&temperature_unit=c&languagecode=en-us&currency_code=AED&location=US"),
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
                var values = JsonConvert.DeserializeObject<BookingViewModel>(body);
                return View(values);
            }


        }
    }
}
