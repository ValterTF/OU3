namespace OU3.Models
{
    public class MovieLoanViewModel
    {
        public MovieLoanViewModel() { }
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Loan> Loans { get; set; }
    }
}
