namespace BingolSinema.Models
{

// Kullanıcı sınıfı
using System.ComponentModel.DataAnnotations;

public class Kullanici
{
    [Key]
    public int KullaniciID { get; set; }
    public required string KullaniciAdi { get; set; }
    public required string Sifre { get; set; }

    // Diğer özellikler eklenebilir
}
}