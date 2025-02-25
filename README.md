Backend: .NET 8.0+
Veritabanı: MSSQL
Mimari: Model – Context – API – Web ile Katmanlı Mimari
Kimlik Doğrulama: JWT Authentication
Swagger Desteği

🚀 İş Gereksinimleri

✔ Kullanıcı Yönetimi:
Kayıt olma (Ad, Soyad, E-posta, Telefon, Şifre, Profil Resmi)
Hoş geldiniz e-postası gönderimi
Giriş yapma (E-posta & Şifre)

✔ Toplantı Yönetimi:
Toplantı CRUD (Oluşturma, Güncelleme, Silme, Listeleme)
Toplantı için belge/doküman yükleme
Toplantı iptali & belirli periyotlarda silinmesi (Hangfire / Quartz vb.)
MSSQL Trigger ile silinen toplantıların loglanması
Toplantı bilgilendirme e-postası gönderimi

✔ Güvenlik:
Parolaların(Salt & Hash) şifrelenerek saklanması
JWT tabanlı kimlik doğrulama

🏗️ Kurulum ve Çalıştırma

1️⃣ Projeyi Klonlayın:

git clone https://github.com/burcinakan/MeetingApp.git

cd MeetingApp

✔ Veritabanı Migrasyonu (MSSQL)

appsettings.json dosyasına localdb ile erişim sağlandı.

🔹 Veritabanı trigger ve script dosyaları Context/Scripts içerisine eklendi.


🔹 Migration İşlemleri
1️⃣ Yeni Migration Eklemek İçin:
📌 Not: Eğer daha önce add-migration işlemi yaptıysan, bu adımı atlayabilirsin.



