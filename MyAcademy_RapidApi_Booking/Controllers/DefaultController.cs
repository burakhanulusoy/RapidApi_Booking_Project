using Microsoft.AspNetCore.Mvc;

namespace MyAcademy_RapidApi_Booking.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
