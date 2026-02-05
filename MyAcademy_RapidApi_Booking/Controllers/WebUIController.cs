using Microsoft.AspNetCore.Mvc;

namespace MyAcademy_RapidApi_Booking.Controllers
{
    public class WebUIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
