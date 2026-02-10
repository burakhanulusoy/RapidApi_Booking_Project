using Microsoft.AspNetCore.Mvc;
using MyAcademy_RapidApi_Booking.Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MyAcademy_RapidApi_Booking.Controllers
{
    public class BookingController : Controller
    {
        public async Task<IActionResult> Index(string city, string arrivalDate, string departureDate, string adults)
        {
            // Eğer parametreler boş gelirse varsayılan değerler ata (Hata almamak için)
            if (string.IsNullOrEmpty(city)) city = "Istanbul";
            if (string.IsNullOrEmpty(arrivalDate)) arrivalDate = DateTime.Now.ToString("yyyy-MM-dd");
            if (string.IsNullOrEmpty(departureDate)) departureDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            if (string.IsNullOrEmpty(adults)) adults = "1";
            ViewBag.arrivalDate = arrivalDate;
            ViewBag.departureDate = departureDate;
            ViewBag.adults = adults;
            var client = new HttpClient();
            var apiHost = "booking-com15.p.rapidapi.com";
            var apiKey = "7fdc49d2efmshc7819949be8e35ep10538fjsn9132de7d9d84"; 

            // ----------------------------------------------------------------
            // ADIM 1: Şehir isminden Destination ID (dest_id) Bulma
            // ----------------------------------------------------------------
            string destId = "";
            string destType = "";

            var requestDest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchDestination?query={city}"),
                Headers =
                {
                    { "x-rapidapi-key", apiKey },
                    { "x-rapidapi-host", apiHost },
                },
            };

            using (var responseDest = await client.SendAsync(requestDest))
            {
                responseDest.EnsureSuccessStatusCode();
                var bodyDest = await responseDest.Content.ReadAsStringAsync();

                // Gelen JSON'u senin DestinationIdViewModel modeline çeviriyoruz
                var valuesDest = JsonConvert.DeserializeObject<DestinationIdViewModel>(bodyDest);

                // İlk gelen sonucun dest_id'sini alıyoruz
                if (valuesDest.data != null && valuesDest.data.Length > 0)
                {
                    destId = valuesDest.data[0].dest_id;
                    destType= valuesDest.data[0].dest_type;
                }
                else
                {
                    // Şehir bulunamazsa boş dönebilir veya hata sayfası gösterebilirsin
                    return View(new List<BookingViewModel.Hotel>());
                }
            }

            // ----------------------------------------------------------------
            // ADIM 2: Bulunan dest_id ile Otelleri Listeleme
            // ----------------------------------------------------------------

            // API Tarih formatı yyyy-MM-dd olmalı. Gelen veri bu formatta değilse düzeltmen gerekebilir.
            var requestHotel = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchHotels?dest_id={destId}&search_type={destType}&adults={adults}&children_age=0%2C17&room_qty=1&page_number=1&units=metric&temperature_unit=c&languagecode=en-us&currency_code=TRY&arrival_date={arrivalDate}&departure_date={departureDate}"),
                Headers =
                {
                    { "x-rapidapi-key", apiKey },
                    { "x-rapidapi-host", apiHost },
                },
            };

            using (var responseHotel = await client.SendAsync(requestHotel))
            {
                responseHotel.EnsureSuccessStatusCode();
                var bodyHotel = await responseHotel.Content.ReadAsStringAsync();

                // Gelen JSON'u BookingViewModel modeline çeviriyoruz
                var valuesHotel = JsonConvert.DeserializeObject<BookingViewModel>(bodyHotel);

                // View'e sadece otel listesini gönderiyoruz
                ViewBag.City = city; // Şehir bilgisini de View'e gönderebiliriz
                return View(valuesHotel.data.hotels);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Filter(string city, string type, string dates, int room_qty, int adults, int children, int min_price, int max_price)
        {
            // 1. ŞEHİR YOKSA DÖN
            if (string.IsNullOrEmpty(city)) return RedirectToAction("Index");

            
            ViewBag.City = city;
            ViewBag.Type = type;
            // 2. SAYI KONTROLLERİ
            if (adults <= 0) adults = 1;
            if (room_qty <= 0) room_qty = 1;
            if (max_price <= 0) max_price = 5000;

            // --- TARİH AYARLAMASI (BURASI KİLİT NOKTA) ---

            // Önce boş tanımlıyoruz, varsayılan atamıyoruz ki hatayı görelim.
            string arrivalDateApi = "";
            string departureDateApi = "";

            if (!string.IsNullOrEmpty(dates))
            {
                // Gelen veri: "02/14/2026 - 02/21/2026" (Ay/Gün/Yıl)
                var dateParts = dates.Split('-');
                if (dateParts.Length >= 2)
                {
                    string rawArrival = dateParts[0].Trim();
                    string rawDeparture = dateParts[1].Trim();

                    DateTime d1, d2;

                    // PC DİLİ NE OLURSA OLSUN, BUNU ABD FORMATI (Ay/Gün/Yıl) OLARAK OKU
                    bool p1 = DateTime.TryParseExact(rawArrival, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d1);
                    bool p2 = DateTime.TryParseExact(rawDeparture, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d2);

                    if (p1 && p2)
                    {
                        // OKUDUYSAN API FORMATINA ÇEVİR (Yıl-Ay-Gün)
                        arrivalDateApi = d1.ToString("yyyy-MM-dd");
                        departureDateApi = d2.ToString("yyyy-MM-dd");
                    }
                }
            }

            // EĞER HALA BOŞSA (Yani tarih parse edilemediyse) O ZAMAN BUGÜNÜ VER (Mecburen)
            if (string.IsNullOrEmpty(arrivalDateApi))
            {
                arrivalDateApi = DateTime.Now.ToString("yyyy-MM-dd");
                departureDateApi = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            }
            ViewBag.arrivalDate = arrivalDateApi;
            ViewBag.departureDate = departureDateApi;
            ViewBag.adults = adults;
            // --- API İŞLEMLERİ ---
            var client = new HttpClient();
            var apiHost = "booking-com15.p.rapidapi.com";
            var apiKey = "7fdc49d2efmshc7819949be8e35ep10538fjsn9132de7d9d84";

            try
            {
                // ADIM 1: Destination ID Bulma
                string destId = "";
                string destType = "";

                var requestDest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchDestination?query={city}"),
                    Headers = { { "x-rapidapi-key", apiKey }, { "x-rapidapi-host", apiHost } },
                };

                using (var responseDest = await client.SendAsync(requestDest))
                {
                    responseDest.EnsureSuccessStatusCode();
                    var bodyDest = await responseDest.Content.ReadAsStringAsync();
                    var valuesDest = JsonConvert.DeserializeObject<DestinationIdViewModel>(bodyDest);

                    if (valuesDest?.data != null && valuesDest.data.Any())
                    {
                        var targetLocation = valuesDest.data.FirstOrDefault(x => x.search_type.Equals(type,StringComparison.OrdinalIgnoreCase));

                        

                        if (targetLocation == null)
                        {
                            targetLocation = valuesDest.data[0];
                        }

                        destId = targetLocation.dest_id;
                        destType = targetLocation.search_type;
                    }
                    else
                    {
                        return View("Index", new List<BookingViewModel.Hotel>());
                    }
                }

                // ADIM 2: Otel Arama (BURADA ARTIK PARSE EDİLMİŞ TARİHLER VAR)
                var requestUrl = $"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchHotels?dest_id={destId}&search_type={destType}&adults={adults}&children_age=0%2C17&room_qty=1&page_number=1&units=metric&temperature_unit=c&languagecode=en-us&currency_code=TRY&arrival_date={arrivalDateApi}&departure_date={departureDateApi}&price_min={min_price}&price_max={max_price}";

                var requestHotel = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(requestUrl),
                    Headers = { { "x-rapidapi-key", apiKey }, { "x-rapidapi-host", apiHost } },
                };

                using (var responseHotel = await client.SendAsync(requestHotel))
                {
                    responseHotel.EnsureSuccessStatusCode();
                    var bodyHotel = await responseHotel.Content.ReadAsStringAsync();
                    var valuesHotel = JsonConvert.DeserializeObject<BookingViewModel>(bodyHotel);

                    if (valuesHotel != null && valuesHotel.data != null && valuesHotel.data.hotels != null)
                    {
                        // --- BURASI DÜZELTİLDİ: C# İLE FİLTRELEME ---

                        // API'den gelen listeyi al
                        var filteredHotels = valuesHotel.data.hotels;

                        // Eğer kullanıcı min_price gönderdiyse filtrele
                        if (min_price > 0)
                        {
                            filteredHotels = filteredHotels.Where(x => x.property.priceBreakdown.grossPrice.value >= min_price).ToList();
                        }

                        // Eğer kullanıcı max_price gönderdiyse filtrele
                        if (max_price > 0)
                        {
                            filteredHotels = filteredHotels.Where(x => x.property.priceBreakdown.grossPrice.value <= max_price).ToList();
                        }

                        // View'a artık filtrelenmiş listeyi gönderiyoruz
                        return View("Index", filteredHotels);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Otel bulunamadı.";
                        return View("Index", new List<BookingViewModel.Hotel>());
                    }
                }
            }
            catch
            {
                return View("Index", new List<BookingViewModel.Hotel>());
            }
        }

      



        public async Task<IActionResult> hotelDetails(string id, string arrivalDate, string departureDate, string adults)
        {



            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/getHotelDetails?hotel_id={id}&arrival_date={arrivalDate}&departure_date={departureDate}&adults={adults}&units=metric&temperature_unit=c&languagecode=en-us&currency_code=TRY"),
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
                var values=JsonConvert.DeserializeObject<GetHotelDetailViewModel>(body);
                return View(values.data);

            }



        }






        }
}