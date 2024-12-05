namespace OU3.Models
{
    public class Loan
    {
        public Loan() { }
        public int LoanID { get; set; }
        public int FilmID { get; set; } 
        public string BorrowerName { get; set; }
        public DateTime LoanDate { get; set; } 
        public DateTime? ReturnDate { get; set; }
    }
}
