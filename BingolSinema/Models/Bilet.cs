namespace BingolSinema.Models
{

// Bilet sınıfı
using System.ComponentModel.DataAnnotations;

public class Bilet
{
    [Key]
    public int BiletID { get; set; }
    public int RezervasyonID { get; set; }
    public decimal Fiyat { get; set; }

    // Diğer özellikler eklenebilir
}

}