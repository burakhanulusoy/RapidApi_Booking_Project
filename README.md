

# 🚀 RapidAPI & Gemini AI Integrated Web Platform

![.NET 8.0](https://img.shields.io/badge/.NET-8.0-purple?style=flat&logo=dotnet) ![Gemini AI](https://img.shields.io/badge/Google-Gemini_2.5_Pro-blue?style=flat&logo=google) ![RapidAPI](https://img.shields.io/badge/RapidAPI-Integrated-green?style=flat&logo=rapid) ![Status](https://img.shields.io/badge/Status-Completed-success?style=flat)

[![Watch Demo Video](https://img.shields.io/badge/▶_-Proje_Demo_Videosunu_İzle-red?style=for-the-badge&logo=youtube&logoColor=white)](BURAYA_VİDEO_LİNKİNİ_YAPIŞTIR)

---

Bu proje, **.NET 8.0** mimarisi üzerinde geliştirilmiş; seyahat, finans ve eğlence sektörlerine ait verileri tek bir çatı altında toplayan ve **Yapay Zeka Destekli (Auto Agent)** asistan hizmeti sunan kapsamlı bir web uygulamasıdır. 

Tasarım aşamasında **Stitch AI** desteği alınmış, veri akışı için **RapidAPI** altyapısı ve **Google Gemini 2.5 Pro** modeli kullanılmıştır.

---

## 🌟 Öne Çıkan Özellikler

Proje 4 ana modül ve 1 özel AI Agent sisteminden oluşmaktadır:

### 1. 🤖 Gemini Auto Agent (FIN-BERT)
Sıradan bir chatbot değil, özel olarak tasarlanmış bir **Finansal Analist** kişiliğidir.
- **Model:** Google Gemini 2.5 Pro
- **Kabiliyet:** Yalnızca borsa, kripto para, teknik/temel analiz sorularına yanıt verir.
- **Güvenlik & Prompt Engineering:** "System Instructions" ile sınırlandırılmıştır. Finans dışı soruları (örneğin "Nasılsın?", "Yemek tarifi ver") reddeder.
- **Persona:** Cyberpunk evreninden gelen "FIN-BERT" kod adlı bir terminal asistanı gibi konuşur.

### 2. 🏨 Otel Rezervasyon Modülü (Booking.com API)
- Kullanıcının girdiği Şehir veya Bölge (District) bilgisine göre dinamik arama.
- Otel listeleme, detaylı oda görselleri ve tesis özelliklerinin sunulması.

### 3. 📈 Finansal Veri Takibi (Financial Modeling Prep API)
- **NASDAQ Top 50:** Borsadaki en hareketli 50 hissenin anlık fiyatları ve değişim yüzdeleri.
- **Döviz Kurları:** Booking.com API üzerinden çekilen güncel Türk Lirası (TRY) ve çapraz döviz kurları.

### 4. 🎬 Sinema Veritabanı (IMDb API)
- Tüm zamanların en yüksek puanlı **IMDb Top 100** film listesinin listelenmesi ve detayları.

---

## 🛠 Teknoloji Yığını (Tech Stack)

* **Backend:** ASP.NET Core 8.0 (MVC)
* **AI Entegrasyonu:** Google Generative AI SDK / REST API
* **Veri Kaynakları (APIs):**
    * Booking.com (RapidAPI)
    * IMDb Top 100 Movies (RapidAPI)
    * Financial Modeling Prep (Stock Data)
* **Frontend:** HTML5, CSS3, Bootstrap (Stitch AI destekli tasarım)
* **Veri Formatı:** JSON (Newtonsoft.Json / System.Text.Json)

---

## 🧠 AI Agent Prompt Yapısı

Projenin en güçlü yanı, yapay zekanın "halüsinasyon" görmesini engelleyen ve bağlamda kalmasını sağlayan **Prompt Mühendisliği** yapısıdır.

**Kullanılan Sistem Talimatı (System Context):**
> *"Sen 'FIN-BERT' kod adlı, Cyberpunk evreninden gelen elit bir Borsa ve Finans Analisti yapay zekasısın. SADECE hisse senetleri, borsa, coin piyasaları hakkında konuş. Finans dışı sorularda 'ERİŞİM REDDEDİLDİ' yanıtını ver."*

---

## 📷 Ekran Görüntüleri (Screenshots)

Projeye ait görselleri kategoriler halinde aşağıda bulabilirsiniz. Görüntülemek için başlıklara tıklayınız.

<details>
  <summary><strong>🏠 Ana Sayfa ve Genel Bakış (Tıkla/Aç)</strong></summary>
  <br>
  <img src="https://github.com/user-attachments/assets/4194dc5d-dd2e-4dd4-ac7c-87b3524b3a83" width="800">
  <img src="https://github.com/user-attachments/assets/e45639c1-fa27-4180-9e77-fd134695fc34" width="800">
  <img src="https://github.com/user-attachments/assets/015a8430-2e98-4a2f-959a-58547733a266" width="800">
  <img src="https://github.com/user-attachments/assets/0504e9ed-e783-4319-94f7-414fa1497c0b" width="800">
  <img src="https://github.com/user-attachments/assets/70e1639d-04b6-402d-ba30-beb2971ea45c" width="800">
  <img src="https://github.com/user-attachments/assets/005a25fe-f204-4d9b-a478-6c1e5fa6e540" width="800">
</details>

<details>
  <summary><strong>🏨 Otel Arama ve Listeleme (Tıkla/Aç)</strong></summary>
  <br>
  <p>Şehir bazlı arama ve sonuçların listelenmesi.</p>
  <img src="https://github.com/user-attachments/assets/1e6b1288-53a6-490e-a3f8-df771b33d7a1" width="800">
  <img src="https://github.com/user-attachments/assets/1af4006f-2dd9-4750-b3e3-c715a973119e" width="800">
  <img src="https://github.com/user-attachments/assets/7956d49b-9102-4b44-a514-0b763fb526ff" width="800">
  <img src="https://github.com/user-attachments/assets/0465fe6e-19a8-4aaf-bcaf-0df249674830" width="800">
  <img src="https://github.com/user-attachments/assets/ce5e088b-89c6-4dd4-91ae-0f74215b3c26" width="800">
</details>

<details>
  <summary><strong>🛏️ Otel Detay Sayfaları (Tıkla/Aç)</strong></summary>
  <br>
  <p>Seçilen otele ait detaylı görseller ve bilgiler.</p>
  <img src="https://github.com/user-attachments/assets/384661a6-5561-429b-a26e-213cb84a92a9" width="800">
  <img src="https://github.com/user-attachments/assets/c999ab1c-5551-477d-b5ef-a0951c28e0a0" width="800">
  <img src="https://github.com/user-attachments/assets/d3a2482a-ca2e-4bb1-8259-f2d441f592ff" width="800">
  <img src="https://github.com/user-attachments/assets/e08cfbb0-a711-4329-bc88-ef4fdd104152" width="800">
  <img src="https://github.com/user-attachments/assets/7ced3898-9539-4b29-9e77-9b70aecfa0cb" width="800">
</details>

<details>
  <summary><strong>🤖 Gemini AI Finans Chatbot & NASDAQ Verileri (Tıkla/Aç)</strong></summary>
  <br>
  <p>Finansal Analist AI ve Canlı Borsa Verileri.</p>
  <img src="https://github.com/user-attachments/assets/429940b2-9df4-4531-9c7a-24bd5c87aacd" width="800">
  <img src="https://github.com/user-attachments/assets/57c2e64d-4712-4adb-8382-f2f3941a8fe9" width="800">
  <img src="https://github.com/user-attachments/assets/46d24395-467c-4a19-a2d1-8e40a7920061" width="800">
  <img src="https://github.com/user-attachments/assets/94175937-baa6-480c-bb4a-92ddeee379df" width="800">
  <img src="https://github.com/user-attachments/assets/d56eb9d7-3b9b-4e84-9e98-579398214dd6" width="800">
  <img src="https://github.com/user-attachments/assets/3460ca89-7f7e-49ad-a4bd-4928f9df6c9a" width="800">
</details>

<details>
  <summary><strong>💱 Döviz Kurları ve 🎬 IMDb Top 100 (Tıkla/Aç)</strong></summary>
  <br>
  <img src="https://github.com/user-attachments/assets/31c35d7c-0352-4f85-8b7f-84993a7c2de9" width="800">
  <img src="https://github.com/user-attachments/assets/22f01ec9-9da2-4064-b317-a85c626e7d34" width="800">
  <img src="https://github.com/user-attachments/assets/5c4e7927-5c35-42c1-a2e7-2e2421ec4e0a" width="800">
  <img src="https://github.com/user-attachments/assets/236665ee-5fbf-4548-b889-afebe827a784" width="800">
  <img src="https://github.com/user-attachments/assets/04133756-44b5-4a2b-9fe0-ae5c43e17c42" width="800">
</details>

---

## ⚙️ Kurulum ve Çalıştırma

Projeyi yerel makinenizde çalıştırmak için adımları takip edin:

1.  **Projeyi Klonlayın:**
    ```bash
    git clone [https://github.com/kullaniciadi/proje-isminiz.git](https://github.com/kullaniciadi/proje-isminiz.git)
    cd proje-isminiz
    ```

2.  **API Anahtarlarını Yapılandırın:**
    Proje kök dizininde `appsettings.json` (veya güvenli geliştirme için `secrets.json`) dosyanızı düzenleyin ve ilgili API anahtarlarını girin:

    ```json
    {
      "Gemini": {
        "ApiKey": "BURAYA_GOOGLE_GEMINI_API_KEY_GELECEK"
     }
  
      
    }
    ```

3.  **Bağımlılıkları Yükleyin ve Çalıştırın:**
    ```bash
    dotnet restore
    dotnet run
    ```

4.  **Tarayıcıda Açın:**
    Uygulama genellikle `https://localhost:7000` veya `http://localhost:5000` adresinde çalışacaktır.

---

## 📄 Lisans

Bu proje [MIT](LICENSE) lisansı altında lisanslanmıştır.

---

**Geliştirici Notu:** Bu proje, .NET ekosisteminde **Rapid API** tüketimini öğrenmek amaçlamıştır.
