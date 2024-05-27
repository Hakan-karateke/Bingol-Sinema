using System.ComponentModel.DataAnnotations;

namespace BingolSinema.Models
{
    public class FilmTur
    {
        [Key]
        public int TurID{get; set; }
        public required string Tur{ get; set; }
    }
}