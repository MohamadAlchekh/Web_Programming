# Kampüs Yanımda

Kampüs Yanımda, üniversite öğrencilerinin sosyal ve akademik deneyimlerini geliştirmek için tasarlanmış dinamik bir platformdur. Öğrencilerin topluluklar oluşturmasına, etkinlikler düzenlemesine ve diğer öğrencilerle etkileşime geçmesine olanak tanır.

## Özellikler

- **Topluluk Oluşturma**: Öğrenciler kendi topluluklarını oluşturabilir ve yönetebilir.
- **Etkinlik Yönetimi**: Etkinlik düzenleme ve katılım takibi yapabilirsiniz.
- **Kullanıcı Doğrulama**: Güvenli giriş ve kayıt sistemi.
- **Yönetici Paneli**: Topluluk isteklerini ve etkinlikleri yönetme.
- **Duyarlı Tasarım**: Hem masaüstü hem de mobil cihazlar için optimize edilmiştir.

## Kullanılan Teknolojiler

- **ASP.NET Core MVC**: Web uygulamasını oluşturmak için.
- **Entity Framework Core**: Veritabanı işlemleri için.
- **Bootstrap**: Duyarlı tasarım ve UI bileşenleri için.
- **jQuery**: İstemci tarafı betik ve AJAX istekleri için.

## Proje Yapısı

- **Controllers**: Uygulamanın farklı bölümleri için mantığı yönetir.
  - `ToplulukController.cs`: Toplulukla ilgili işlemleri yönetir. 
    - startLine: 14
    - endLine: 135
  - `NedenToplulukController.cs`: "Neden Topluluk" sayfasını yönetir.
    - startLine: 1
    - endLine: 12
  - `AdminController.cs`: Yönetici işlevselliklerini yönetir.
    - startLine: 102
    - endLine: 165
  - `HomeController.cs`: Ana sayfa ve bilgilendirme sayfalarını yönetir.
    - startLine: 1
    - endLine: 63

- **Views**: HTML çıktısını oluşturmak için Razor görünümlerini içerir.
  - `Views/NedenTopluluk/Index.cshtml`: Platformun faydalarını açıklar.
    - startLine: 1
    - endLine: 91
  - `Views/Topluluk/Detay.cshtml`: Bir topluluk hakkında detaylı bilgi gösterir.
    - startLine: 1
    - endLine: 129
  - `Views/Home/About.cshtml`: Platform hakkında bilgi verir.
    - startLine: 1
    - endLine: 345
  - `Views/Account/Register.cshtml`: Kullanıcı doğrulama sayfaları.

## Kurulum Talimatları

1. **Depoyu Klonlayın**:
   ```bash
   git clone https://github.com/yourusername/kampus-yanimda.git
   cd kampus-yanimda
   ```

2. **Veritabanı Kurulumu**:
   - SQL Server'ın yüklü olduğundan emin olun.
   - `appsettings.json` dosyasındaki bağlantı dizgisini veritabanınıza göre güncelleyin.

3. **Migrasyonları Çalıştırın**:
   ```bash
   dotnet ef database update
   ```

4. **Uygulamayı Çalıştırın**:
   ```bash
   dotnet run
   ```

5. **Uygulamaya Erişin**:
   - Tarayıcınızı açın ve `http://localhost:5000` adresine gidin.

## Katkıda Bulunma

Katkılarınızı bekliyoruz! Lütfen depoyu çatallayın ve herhangi bir iyileştirme veya hata düzeltmesi için bir çekme isteği gönderin.

## Lisans

Bu proje MIT Lisansı altında lisanslanmıştır. Daha fazla bilgi için [LICENSE](LICENSE) dosyasına bakın.

## İletişim

Herhangi bir soru veya geri bildirim için lütfen bize support@kampusyanimda.com adresinden ulaşın.
