using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BingolSinema.Models;

namespace BingolSinema.Controllers
{
    public class AdminController : Controller
    {
        private readonly MyDbContext _context;

        public AdminController(MyDbContext context)
        {
            _context = context;
        }

        // 4. Film ekleyebilme ve seans düzenleyebilme için yönetici paneli
        public IActionResult Index()
        {
            var filmler = _context.Films.ToList();
            return View(filmler);
        }

        public IActionResult FilmEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FilmEkle(Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Films.Add(film);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(film);
        }

        public IActionResult SeansEkle()
        {
            var filmler = _context.Films.ToList();
            var salonlar = _context.Salons.ToList();
            ViewBag.Filmler = filmler;
            ViewBag.Salonlar = salonlar;
            return View();
        }

        [HttpPost]
        public IActionResult SeansEkle(Seans seans)
        {
            if (ModelState.IsValid)
            {
                _context.Seanss.Add(seans);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seans);
        }

        // 5. Yönetici girişi için kimlik doğrulama mekanizması
        public IActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Giris(string adminAdi, string sifre)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.AdminAdi == adminAdi && a.Sifre == sifre);
            if (admin != null)
            {
                // Kimlik doğrulama başarılı
                // Admin oturumunu aç (örneğin, bir session oluştur)
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre");
            return View();
        }

        // 8. İstatistikleri gösteren raporlama ekranı
        public IActionResult Raporlar()
        {
            var filmGosterimSayilari = _context.Seanss.GroupBy(s => s.Film.FilmAdi)
                .Select(g => new { Film = g.Key, GosterimSayisi = g.Count() }).ToList();

            var salonDolulukOranlari = _context.Seanss.GroupBy(s => s.Salon)
                .Select(g => new { Salon = g.Key.SalonAdi, DolulukOrani = (double)g.Sum(s => s.Rezervasyonlar.Count) / g.Key.Kapasite }).ToList();

            var enPopulerFilmler = _context.Seanss.GroupBy(s => s.Film.FilmAdi)
                .OrderByDescending(g => g.Sum(s => s.Rezervasyonlar.Count))
                .Select(g => g.Key).Take(5).ToList();

            ViewBag.FilmGosterimSayilari = filmGosterimSayilari;
            ViewBag.SalonDolulukOranlari = salonDolulukOranlari;
            ViewBag.EnPopulerFilmler = enPopulerFilmler;

            return View();
        }
    }
}