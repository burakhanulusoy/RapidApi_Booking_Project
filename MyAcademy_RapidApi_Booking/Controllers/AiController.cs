using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace MyAcademy_RapidApi_Booking.Controllers
{
    public class AiController : Controller
    {
        private readonly IConfiguration _configuration;

        public AiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public class AiRequest
        {
            public string UserInput { get; set; }
            // EKLENDİ: Konuşmanın devamlılığı için geçmişi tutuyoruz.
            // Frontend tarafında eski mesajları birleştirip buraya göndermelisin.
            public string ConversationHistory { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> GetResponse([FromBody] AiRequest request)
        {
            string apiKey = _configuration["Gemini:ApiKey"];
            string model = "gemini-2.5-pro"; 
            string endpoint = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

            // --- YENİ NESİL AUTO-AGENT PROMPTU ---
            string systemContext = @"
                Sen 'FIN-BERT' kod adlı, Cyberpunk evreninden gelen PROAKTİF bir Borsa ve Finans Ajanısın.
                Sadece cevap veren bir bot değil, yatırımcıyı yönlendiren bir akıl hocasısın.

                GÖREV AKIŞIN ŞU ŞEKİLDEDİR:
                1.  **ANALİZ:** Kullanıcının sorduğu hisse/coin için Teknik ve Temel analiz özetini (RSI, MACD, F/K vb. terimlerle) Cyberpunk ağzıyla yap.
                2.  **YÖNLENDİRME (AUTO-AGENT MODU):** Analizi bitirdikten sonra ASLA susma. Kullanıcıya analizle ilgili derinleşmesi için 3 adet 'Sonraki Hamle' önerisi sun.
                
                ÖRNEK ÇIKTI FORMATIN:
                ---------------------------------------------------
                >> HEDEF TESPİT EDİLDİ: [Hisse Kodu]
                >> SİSTEM ANALİZİ: 
                   - Teknik Göstergeler: [Veriler]
                   - Temel Durum: [Veriler]
                   - Karar: [AL/SAT/TUT]
                
                >> SONRAKİ HAMLE ÖNERİLERİ (Birisini seç):
                1. [Hisse] için son KAP haberlerini tara?
                2. [Hisse] ile rakibi [Rakip Hisse] karşılaştırmasını yap?
                3. Dolar bazlı grafiğine bak?
                ---------------------------------------------------

                KURALLAR:
                - Finans dışı soruları reddet.
                - Her cevabın sonunda mutlaka kullanıcıyı yönlendirecek sorular sor. Sohbeti sen yönet.
            ";

            // Geçmişi ve yeni mesajı birleştiriyoruz
            string contextData = string.IsNullOrEmpty(request.ConversationHistory) ? "" : $"ÖNCEKİ KONUŞMA GEÇMİŞİ:\n{request.ConversationHistory}\n\n";
            string fullPrompt = $"{systemContext}\n\n{contextData}KULLANICI GİRDİSİ: {request.UserInput}";

            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = fullPrompt } } }
                }
            };

            using var client = new HttpClient();
            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(endpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest(new { success = false, message = "Bağlantı Hatası." });
                }

                var responseText = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(responseText);

                var aiResponse = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                // Cevabı temizle (Markdown yıldızlarını vs frontend'de düzgün göstermek için)
                return Ok(new { success = true, message = aiResponse });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Sistem Hatası: " + ex.Message });
            }
        }
    }
}