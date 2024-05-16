namespace BingolSinema.Models
{
    


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Seans
{
    [Key]
    public int SeansID { get; set; }
    public int FilmID { get; set; }
    public int SalonID { get; set; }
    public DateTime BaslangicZamani { get; set; }
    public DateTime BitisZamani { get; set; }

    // İlişkiler
    [ForeignKey("FilmID")]
    public virtual Film Film { get; set; }

    [ForeignKey("SalonID")]
    public virtual Salon Salon { get; set; }

    public virtual ICollection<Rezervasyon> Rezervasyonlar { get; set; }
}

}
