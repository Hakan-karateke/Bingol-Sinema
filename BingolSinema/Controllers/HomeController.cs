using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BingolSinema.Models;
using Microsoft.EntityFrameworkCore;

namespace BingolSinema.Controllers;
    
public class HomeController : Controller
{
        private readonly MyDbContext _context;

        public static Kullanici? GirisYapanKullanici= null;

        public static OdemeViewModel? OdemeViewModelKullanıcı;

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
            // Check if the username already exists
            var existingUser = _context.Kullanicis.FirstOrDefault(u => u.KullaniciAdi == kullanici.KullaniciAdi);
            if (existingUser != null)
            {
                // Username already exists, return an error message
                ModelState.AddModelError("KullaniciAdi", "This username is already taken. Please choose a different username.");
                return View(kullanici);
            }


            GirisYapanKullanici = kullanici;
            _context.Kullanicis.Add(kullanici);
            _context.SaveChanges();
            return RedirectToAction("Index");

            // If model state is not valid, return the view with the current model
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
                GirisYapanKullanici=kullanici;
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

            OdemeViewModelKullanıcı = new OdemeViewModel
            {
                Seans = seans,
                Biletler = biletler,
                Rezervasyon = new Rezervasyon { SeansID = seansId }
            };

            return View(OdemeViewModelKullanıcı);
        }

        [HttpPost]
        public IActionResult Odeme(OdemeViewModel model)
        {
            
            OdemeViewModelKullanıcı.KoltukNumarasi=model.KoltukNumarasi;

            model.Biletler= OdemeViewModelKullanıcı.Biletler;
            model.Seans=OdemeViewModelKullanıcı.Seans;
            model.Rezervasyon= OdemeViewModelKullanıcı.Rezervasyon;


            ///model için eşleştirmeler yapılacak seans 
            if (model.KoltukNumarasi != null )
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
                    return RedirectToAction("Success",rezervasyon.KoltukNumarasi);
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

        public IActionResult Success(int KoltukNumarasi)
        {
            // Action for displaying payment success page
            return View(KoltukNumarasi);
        }

        // 6. Kullanıcıların rezervasyonlarını iptal edebilmesi veya değişiklik yapabilmesi için ekran
        public IActionResult Rezervasyonlarim()
        {
            if (GirisYapanKullanici == null)
            {
                return RedirectToAction("Giris");
            }

            int kullaniciId = GirisYapanKullanici.KullaniciID;
            var kullanici = _context.Kullanicis.Find(kullaniciId);
            var rezervasyonlar = _context.Rezervasyons
                .Where(r => r.KullaniciID == kullaniciId)
                .ToList();

            foreach(var rezervasyon in rezervasyonlar)
            {
                rezervasyon.Seans = _context.Seanss.FirstOrDefault(s => s.SeansID == rezervasyon.SeansID);
                rezervasyon.Seans.Film = _context.Films.FirstOrDefault(s => s.FilmID == rezervasyon.Seans.FilmID);
                rezervasyon.Seans.Salon= _context.Salons.FirstOrDefault(s=> s.SalonID == rezervasyon.Seans.SalonID);
            }

            var viewModel = new UserProfileViewModel
            {
                User = kullanici,
                Reservations = rezervasyonlar
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CancelReservation(int id)
        {
            var rezervasyon = _context.Rezervasyons.Find(id);
            if (rezervasyon != null)
            {
                var bilet = _context.Bilets.FirstOrDefault(b => b.RezervasyonID == id);
                if (bilet != null)
                {
                    _context.Bilets.Remove(bilet);
                }
                _context.Rezervasyons.Remove(rezervasyon);
                _context.SaveChanges();
            }
            return RedirectToAction("Rezervasyonlarim");
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
    
        [HttpPost]
        public IActionResult DownloadProfile()
        {
            if (GirisYapanKullanici == null)
            {
                return RedirectToAction("Giris");
            }

            int kullaniciId = GirisYapanKullanici.KullaniciID;
            var kullanici = _context.Kullanicis.Find(kullaniciId);

            // Generate a file with user profile information
            var profileInfo = $"Name: {kullanici.Ad} {kullanici.Soyad}\n" +
                            $"Username: {kullanici.KullaniciAdi}\n" +
                            $"Age: {kullanici.Yas}\n" +
                            $"Gender: {(kullanici.Cinsiyet ? "Male" : "Female")}";

            var bytes = System.Text.Encoding.UTF8.GetBytes(profileInfo);
            return File(bytes, "application/octet-stream", "UserProfile.txt");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }