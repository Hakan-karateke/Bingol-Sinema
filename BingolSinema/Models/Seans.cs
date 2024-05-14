// Seans sınıfı
using System.ComponentModel.DataAnnotations;

public class Seans
{
    [Key]
    public int SeansID { get; set; }
    public int FilmID { get; set; }
    public int SalonID { get; set; }
    public DateTime BaslangicZamani { get; set; }
    public DateTime BitisZamani { get; set; }

    // Diğer özellikler eklenebilir
}