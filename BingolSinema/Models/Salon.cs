namespace BingolSinema.Models
{

using System.ComponentModel.DataAnnotations;

public class Salon
{
    [Key]
    public int SalonID { get; set; }
    public required string SalonAdi { get; set; }
    public int Kapasite { get; set; }

    // İlişkiler
    public virtual ICollection<Seans>? Seanslar { get; set; }
}

}
