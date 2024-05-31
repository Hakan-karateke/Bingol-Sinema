using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BingolSinema.Models;

namespace BingolSinema.Controllers
{
    public class AdminController : Controller
    {
        private readonly MyDbContext _context;
        public static Admin? GirisYapanAdmin = null;

        public AdminController(MyDbContext context)
        {
            _context = context;
        }

        // 4. Film ekleyebilme ve seans düzenleyebilme için yönetici paneli
        public IActionResult Index()
        {
            if(GirisYapanAdmin==null)
            {
                return RedirectToAction("Giris");
            }
            var viewModel = new AdminIndexViewModel
            {
                Films = _context.Films.ToList(),
                Salons = _context.Salons.ToList(),
                Seanslar = _context.Seanss.ToList()
            };
            return View(viewModel);
        }


        // GET: Admin/ListUsers
        public IActionResult ListUsers()
        {
            var users = _context.Kullanicis.ToList();
            return View(users);
        }

        // POST: Admin/DeleteUser/5
        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Kullanicis.Find(id);
            if (user != null)
            {
                var rezervasyonlar = _context.Rezervasyons.Where(r => r.KullaniciID == id).ToList();
                foreach (var rezervasyon in rezervasyonlar)
                {
                    var bilet = _context.Bilets.FirstOrDefault(b => b.RezervasyonID == rezervasyon.RezervasyonID);
                    if (bilet != null)
                    {
                        _context.Bilets.Remove(bilet);
                    }
                    _context.Rezervasyons.Remove(rezervasyon);
                }
                _context.Kullanicis.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction("ListUsers");
        }

        // GET: Admin/UpdateFilm/5// GET: Admin/UpdateFilm/5
        public IActionResult UpdateFilm(int id)
        {
            var film = _context.Films.FirstOrDefault(s => s.FilmID == id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        // POST: Admin/UpdateFilm/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateFilm(int id, Film film)
        {
            if (ModelState.IsValid)
            {
                var existingFilm = _context.Films.Find(id);
                if (existingFilm != null)
                {
                    existingFilm.FilmAdi = film.FilmAdi;
                    existingFilm.Yönetmen = film.Yönetmen;
                    existingFilm.FilmResimUrl = film.FilmResimUrl;
                    existingFilm.Yil = film.Yil;
                    existingFilm.TurID = film.TurID;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(film);
        }
        
        // GET: Admin/UpdateSalon/5
        public IActionResult UpdateSalon(int id)
        {
            var salon = _context.Salons.FirstOrDefault(s => s.SalonID == id);
            if (salon == null)
            {
                return NotFound();
            }
            return View(salon);
        }

        // POST: Admin/UpdateSalon/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSalon(int id, Salon salon)
        {
            if (ModelState.IsValid)
            {
                var existingSalon = _context.Salons.Find(id);
                if (existingSalon != null)
                {
                    existingSalon.SalonAdi = salon.SalonAdi;
                    existingSalon.Kapasite = salon.Kapasite;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(salon);
        }
        
        
        // GET: Admin/UpdateSeans/5
        public IActionResult UpdateSeans(int id)
        {
            var seans = _context.Seanss.FirstOrDefault(s => s.SeansID == id);
            if (seans == null)
            {
                return NotFound();
            }
            return View(seans);
        }

        // POST: Admin/UpdateSeans/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSeans(int id, Seans seans)
        {
            if (ModelState.IsValid)
            {
                var existingSeans = _context.Seanss.Find(id);
                if (existingSeans != null)
                {
                    existingSeans.FilmID = seans.FilmID;
                    existingSeans.SalonID = seans.SalonID;
                    existingSeans.BaslangicZamani = seans.BaslangicZamani;
                    existingSeans.BitisZamani = seans.BitisZamani;
                    existingSeans.SeansFiyat = seans.SeansFiyat;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(seans);
        }

        [HttpPost]
        public IActionResult DeleteFilm(int id)
        {
            var film = _context.Films.Find(id);
            if (film != null)
            {
                var seanslar = _context.Seanss.Where(s => s.FilmID == id).ToList();
                foreach (var seans in seanslar)
                {
                    var rezervasyonlar = _context.Rezervasyons.Where(r => r.SeansID == seans.SeansID).ToList();
                    foreach (var rezervasyon in rezervasyonlar)
                    {
                        var bilet = _context.Bilets.FirstOrDefault(b => b.RezervasyonID == rezervasyon.RezervasyonID);
                        if (bilet != null)
                        {
                            _context.Bilets.Remove(bilet);
                        }
                        _context.Rezervasyons.Remove(rezervasyon);
                    }
                    _context.Seanss.Remove(seans);
                }
                _context.Films.Remove(film);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteSalon(int id)
        {
            var salon = _context.Salons.Find(id);
            if (salon != null)
            {
                var seanslar = _context.Seanss.Where(s => s.SalonID == id).ToList();
                foreach (var seans in seanslar)
                {
                    var rezervasyonlar = _context.Rezervasyons.Where(r => r.SeansID == seans.SeansID).ToList();
                    foreach (var rezervasyon in rezervasyonlar)
                    {
                        var bilet = _context.Bilets.FirstOrDefault(b => b.RezervasyonID == rezervasyon.RezervasyonID);
                        if (bilet != null)
                        {
                            _context.Bilets.Remove(bilet);
                        }
                        _context.Rezervasyons.Remove(rezervasyon);
                    }
                    _context.Seanss.Remove(seans);
                }
                _context.Salons.Remove(salon);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteSeans(int id)
        {
            var seans = _context.Seanss.Find(id);
            if (seans != null)
            {
                var rezervasyonlar = _context.Rezervasyons.Where(r => r.SeansID == seans.SeansID).ToList();
                foreach (var rezervasyon in rezervasyonlar)
                {
                    var bilet = _context.Bilets.FirstOrDefault(b => b.RezervasyonID == rezervasyon.RezervasyonID);
                    if (bilet != null)
                    {
                        _context.Bilets.Remove(bilet);
                    }
                    _context.Rezervasyons.Remove(rezervasyon);
                }
                _context.Seanss.Remove(seans);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public IActionResult FilmEkle()
        {
            if(GirisYapanAdmin==null)
            {
                return RedirectToAction("Giris");
            }
            var filmTurler = _context.FilmTurs.ToList();
            ViewBag.FilmTurler= filmTurler;
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
            if(GirisYapanAdmin==null)
            {
                return RedirectToAction("Giris");
            }
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
                GirisYapanAdmin=admin;
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre");
            return View();
        }

        // 8. İstatistikleri gösteren raporlama ekranı
        public IActionResult Raporlar()
        {
            // Popular movies sorted by booking count
            var popularMovies = _context.Rezervasyons
                .GroupBy(r => r.Seans.Film)
                .Select(g => new MoviePopularity
                {
                    Movie = g.Key,
                    BookingCount = g.Count()
                })
                .OrderByDescending(mp => mp.BookingCount)
                .ToList();

            // Hall occupancy rates
            var hallOccupancies = _context.Salons
                .Select(s => new HallOccupancy
                {
                    Hall = s,
                    OccupancyRate = (double)_context.Rezervasyons.Count(r => r.Seans.SalonID == s.SalonID) / (s.Kapasite * _context.Seanss.Count(se => se.SalonID == s.SalonID)) * 100
                })
                .ToList();

            // Movies sorted by release year
            var moviesByReleaseYear = _context.Films
                .OrderBy(f => f.Yil)
                .ToList();

            var reportViewModel = new ReportViewModel
            {
                PopularMovies = popularMovies,
                HallOccupancies = hallOccupancies,
                MoviesByReleaseYear = moviesByReleaseYear
            };

            return View(reportViewModel);
        }


        // New actions for SalonEkle
        public IActionResult SalonEkle()
        {
            
            if(GirisYapanAdmin==null)
            {
                return RedirectToAction("Giris");
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult SalonEkle(Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.Salons.Add(salon);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salon);
        }
       

         // New actions for AdminOl (Admin Sign Up)
        public IActionResult AdminOl()
        {
            if(GirisYapanAdmin==null)
            {
                return RedirectToAction("Giris");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AdminOl(Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.KayitTarihi = DateTime.Now; // Set the registration date
                _context.Admins.Add(admin);
                _context.SaveChanges();
                return RedirectToAction("Giris"); // Redirect to the login page after successful registration
            }
            return View(admin);
        }

        // New actions for TurEkle (Add Film Type)
        public IActionResult TurEkle()
        {
            if(GirisYapanAdmin == null)
            {
                return RedirectToAction("Giris");
            }
            return View();
        }

        [HttpPost]
        public IActionResult TurEkle(FilmTur filmTur)
        {
            if (ModelState.IsValid)
            {
                _context.FilmTurs.Add(filmTur);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(filmTur);
        }
    }
}
