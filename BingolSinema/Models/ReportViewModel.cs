namespace BingolSinema.Models
{
    public class ReportViewModel
    {
        public List<MoviePopularity> PopularMovies { get; set; }
        public List<HallOccupancy> HallOccupancies { get; set; }
        public List<Film> MoviesByReleaseYear { get; set; }
    }

    public class MoviePopularity
    {
        public Film Movie { get; set; }
        public int BookingCount { get; set; }
    }

    public class HallOccupancy
    {
        public Salon Hall { get; set; }
        public double OccupancyRate { get; set; }
    }
}
