namespace OU3.Models
{
    public class MovieLoan
    {
        public MovieLoan() { }

        public int FilmID { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
        public string BorrowerName { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}
