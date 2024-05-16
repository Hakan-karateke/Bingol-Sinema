namespace BingolSinema.Models
{
using System.ComponentModel.DataAnnotations;

public class FilmTur
{
    [Key]
    public int TurID{get; set; }
    public required string Tur{ get; set; }
}
}