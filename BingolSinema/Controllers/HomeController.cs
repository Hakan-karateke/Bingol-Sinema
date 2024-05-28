using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BingolSinema.Models;
using Microsoft.EntityFrameworkCore;

namespace BingolSinema.Controllers;
    
public class HomeController : Controller
{
        private readonly MyDbContext _context;

        public Kullanici? GirisYapanKullanici= null;

        public HomeController(MyDbContext context)
        {
            _context = context;
        }


        // GET: UyeOl
        public IActionResult UyeOl()
        {
            return View();
        }

        // POST: UyeOl
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UyeOl(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                _context.Kullanicis.Add(kullanici);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kullanici);
        }

        // 5. Kullanıcı girişi için kimlik doğrulama mekanizması
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
            foreach (var seans in seanslar)
            {
                // Retrieve the Salon object corresponding to the Seans
                seans.Salon = _context.Salons.FirstOrDefault(s => s.SalonID == seans.SalonID);
                seans.Film = _context.Films.FirstOrDefault(s=> s.FilmID == seans.FilmID);
            }


            
            return View(seanslar);
        }

        // 3. Bilet satın alma işlemi için ödeme ekranı
        public IActionResult Odeme(int seansId, int koltukNumarasi)
        {
            var seans = _context.Seanss.Include(s => s.Salon).FirstOrDefault(s => s.SeansID == seansId);
            if (seans == null)
            {
                return NotFound();
            }

            var rezervasyonlar = _context.Rezervasyons.Where(r => r.SeansID == seansId).ToList();
            var biletler = _context.Bilets.Where(b => rezervasyonlar.Select(r => r.RezervasyonID).Contains(b.RezervasyonID)).ToList();

            var viewModel = new OdemeViewModel
            {
                Seans = seans,
                Biletler = biletler,
                Rezervasyon = new Rezervasyon { SeansID = seansId }
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Odeme(OdemeViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the selected seat is available
                var isSeatAvailable = !_context.Bilets
                    .Any(b => b.Rezervasyon.SeansID == model.Seans.SeansID && b.Rezervasyon.KoltukNumarasi == model.KoltukNumarasi);

                if (isSeatAvailable)
                {
                    // Create a new reservation and ticket
                    var rezervasyon = new Rezervasyon
                    {
                        SeansID = model.Seans.SeansID,
                        KullaniciID = 1, // Assuming there's a logged-in user, replace with actual user ID
                        KoltukNumarasi = model.KoltukNumarasi
                    };
                    _context.Rezervasyons.Add(rezervasyon);
                    _context.SaveChanges();

                    var bilet = new Bilet
                    {
                        RezervasyonID = rezervasyon.RezervasyonID,
                        Fiyat = model.Seans.SeansFiyat
                    };
                    _context.Bilets.Add(bilet);
                    _context.SaveChanges();

                    // Redirect to a success page or show a success message
                    return RedirectToAction("Success");
                }
                else
                {
                    // Seat is already reserved, display error message
                    ModelState.AddModelError(string.Empty, "Seçtiğiniz koltuk zaten alınmış.");
                }
            }

            // If model state is not valid or seat is not available, return to the payment page
            return View("Odeme", model);
        }

        public IActionResult Success()
        {
            // Action for displaying payment success page
            return View();
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