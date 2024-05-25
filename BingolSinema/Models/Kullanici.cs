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
    public required bool Cinsiyet{get; set;}
    public required string Ad {get; set;}
    public required string Soyad {get;set;}
    public required int Yas{get;set;}

    // Diğer özellikler eklenebilir
}
}