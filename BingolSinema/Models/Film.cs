namespace BingolSinema.Models
{
// Film sınıfı
using System.ComponentModel.DataAnnotations;

public class Film
{
    [Key]
    public int FilmID { get; set; }
    public required string FilmAdi { get; set; }
    public required string Yönetmen { get; set; }
    public required string FilmResimUrl{get; set;}
    public int Yil { get; set; }
    public  int TurID { get; set; }

    // Diğer özellikler eklenebilir
}
}