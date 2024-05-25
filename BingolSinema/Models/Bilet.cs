namespace BingolSinema.Models
{

// Bilet sınıfı
using System.ComponentModel.DataAnnotations;

public class Bilet
{
    [Key]
    public int BiletID { get; set; }
    public required int RezervasyonID { get; set; }
    public required double Fiyat { get; set; }

    // Diğer özellikler eklenebilir
}

}