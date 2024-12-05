using Microsoft.AspNetCore.Mvc;
using OU3.Models;

namespace OU3.Controllers
{
    public class LoanController : Controller
    {
        // Visa startsidan för Loan
        public IActionResult Index()
        {
            return View();
        }

        // GET: Visa formulär för att lägga till ett nytt lån
        public IActionResult InsertLoan()
        {
            Loan loan = new Loan();
            return View(loan);
        }

        // POST: Hantera formulärdata och lägg till ett nytt lån
        [HttpPost]
        public IActionResult InsertLoan(int FilmID, string BorrowerName, DateTime LoanDate, DateTime? ReturnDate)
        {
            Loan loan = new Loan
            {
                FilmID = FilmID,
                BorrowerName = BorrowerName,
                LoanDate = LoanDate,
                ReturnDate = ReturnDate
            };

            LoanMethods loanMethods = new LoanMethods();
            string error = "";
            int i = loanMethods.InsertLoan(loan, out error);

            ViewBag.Error = error;
            ViewBag.Antal = i;

            return View();
        }

        // GET: Visa en lista med alla lån
        public IActionResult SelectLoans()
        {
            List<Loan> loanList = new List<Loan>();
            LoanMethods loanMethods = new LoanMethods();
            string error = "";
            loanList = loanMethods.GetLoanList(out error);
            ViewBag.Error = error;
            return View(loanList);
        }
    }
}

