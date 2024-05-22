using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BingolSinema.Models;

namespace BingolSinema.Controllers;
    
public class HomeController : Controller
{
        private readonly MyDbContext _context;

        public Kullanici? GirisYapanKullanici= null;

        public HomeController(MyDbContext context)
        {
            _context = context;
        }

        // 5. Yönetici girişi için kimlik doğrulama mekanizması
        public IActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Giris(string KullaniciAdi, string Sifre)
        {
            var kullanici = _context.Kullanicis.FirstOrDefault(a => a.KullaniciAdi == KullaniciAdi && a.Sifre == Sifre);
            if (kullanici != null)
            {
                // Kimlik doğrulama başarılı
                // Admin oturumunu aç (örneğin, bir session oluştur)
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre");
            return View();
        }

    // 1. Kullanıcıların film seçimini yapabilecekleri ve seansları görebilecekleri ekran
    public IActionResult Index()
        {
            if(GirisYapanKullanici==null)
            {
                RedirectToAction("Giris");
            }
            var filmler = _context.Films.ToList();
            return View(filmler);
        }

        // 2. Seçilen film ve seans için koltuk seçimi yapılabilen arayüz
        public IActionResult Seanslar(int filmId)
        {
            var seanslar = _context.Seanss.Where(s => s.FilmID == filmId).ToList();
            return View(seanslar);
        }

        // 3. Bilet satın alma işlemi için ödeme ekranı
        public IActionResult Odeme(int seansId, int koltukNumarasi)
        {
            var seans = _context.Seanss.Find(seansId);
            var rezervasyon = new Rezervasyon
            {
                SeansID = seansId,
                KoltukNumarasi = koltukNumarasi,
                //KullaniciID = _context.Kullanici.KullaniciID// Kullanıcı kimliği burada belirlenmeli
            };
            return View(rezervasyon);
        }

        [HttpPost]
        public IActionResult Odeme(Rezervasyon rezervasyon, decimal fiyat)
        {
            if (ModelState.IsValid)
            {
                _context.Rezervasyons.Add(rezervasyon);
                _context.SaveChanges();

                var bilet = new Bilet
                {
                    RezervasyonID = rezervasyon.RezervasyonID,
                    Fiyat = fiyat
                };
                _context.Bilets.Add(bilet);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(rezervasyon);
        }

        // 6. Kullanıcıların rezervasyonlarını iptal edebilmesi veya değişiklik yapabilmesi için ekran
        public IActionResult Rezervasyonlarim()
        {
            int kullaniciId = 0; //_context.Kullanici.KullaniciID;// Kullanıcı kimliği burada belirlenmeli
            List<Rezervasyon> Rezervasyonlarim = _context.Rezervasyons.Where(r => r.KullaniciID == kullaniciId).ToList();
            return View(Rezervasyonlarim);
        }

        public IActionResult IptalEt(int rezervasyonId)
        {
            var rezervasyon = _context.Rezervasyons.Find(rezervasyonId);
            if (rezervasyon != null)
            {
                var bilet = _context.Bilets.FirstOrDefault(b => b.RezervasyonID == rezervasyonId);
                if (bilet != null)
                {
                    _context.Bilets.Remove(bilet);
                }
                _context.Rezervasyons.Remove(rezervasyon);
                _context.SaveChanges();
            }
            return RedirectToAction("Rezervasyonlarim");
        }
    }