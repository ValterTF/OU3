namespace OU3.Models
{
    public class Movie
    {
        public Movie() { }

        public int FilmID { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
        public int CopiesAvailable { get; set; }
    }
}
