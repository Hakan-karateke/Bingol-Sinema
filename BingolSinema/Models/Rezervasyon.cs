namespace BingolSinema.Models
{
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Rezervasyon
{
    [Key]
    public int RezervasyonID { get; set; }
    public int KullaniciID { get; set; }
    public int SeansID { get; set; }
    public int KoltukNumarasi { get; set; }

    // İlişkiler
    [ForeignKey("SeansID")]
    public virtual Seans? Seans { get; set; }
}

}
