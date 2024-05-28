using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BingolSinema.Models
{
    // Bilet sınıfı
    public class Bilet
    {
        [Key]
        public int BiletID { get; set; }
        public int RezervasyonID { get; set; } // Changed from 'required' to 'int'
        public int Fiyat { get; set; }

        // Navigation property to Rezervasyon
        [ForeignKey("RezervasyonID")]
        public virtual Rezervasyon Rezervasyon { get; set; } // Ensure Rezervasyon class is defined

        // Diğer özellikler eklenebilir
    }
}
