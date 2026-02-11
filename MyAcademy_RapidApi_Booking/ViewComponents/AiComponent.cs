using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MyAcademy_RapidApi_Booking.ViewComponents
{
    public class AiComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            return View();
        }






    }
}
