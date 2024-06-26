namespace BingolSinema.Models
{
//admin sınıfı

using System.ComponentModel.DataAnnotations;

public class Admin
{
    [Key]
    public int AdminID { get; set; }
    public required string AdminAdi { get; set; }
    public required string Adi{get; set;}
    public required string Soyadi{get; set;}
    public required int Yas{get;set;}
    public required string Sifre { get; set; }
    public bool Cinsiyet{get; set; }
    public DateTime KayitTarihi { get; set; }
}
}