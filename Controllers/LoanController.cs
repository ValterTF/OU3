using Microsoft.AspNetCore.Mvc;
using OU3.Models;

namespace OU3.Controllers
{
    public class LoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertLoan()
        {
            Loan loan = new Loan();
            return View(loan);
        }

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

