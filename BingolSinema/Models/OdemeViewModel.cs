namespace BingolSinema.Models
{

    public class OdemeViewModel
{
    public Seans Seans { get; set; }
    public List<Bilet> Biletler { get; set; }
    public Rezervasyon Rezervasyon { get; set; }
    public int KoltukNumarasi { get; set; } // Add this property
}

}