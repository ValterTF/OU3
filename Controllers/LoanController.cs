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

        public IActionResult EditLoan(int id)
        {
            LoanMethods loanMethods = new LoanMethods();
            string error;
            Loan loan = loanMethods.GetLoanDetails(id, out error);

            if (loan == null)
            {
                ViewBag.Error = error;
                return View("Error");
            }

            return View(loan);
        }

        [HttpPost]
        public IActionResult EditLoan(Loan loan)
        {
            LoanMethods loanMethods = new LoanMethods();
            string error;
            int rowsAffected = loanMethods.UpdateLoan(loan, out error);

            if (rowsAffected == 1)
            {
                return RedirectToAction("SelectLoans");
            }
            else
            {
                ViewBag.Error = error;
                return View(loan);
            }
        }

        public IActionResult DeleteLoan(int id)
        {
            LoanMethods loanMethods = new LoanMethods();
            string error;
            Loan loan = loanMethods.GetLoanDetails(id, out error);

            if (loan == null)
            {
                ViewBag.Error = error;
                return View("Error");
            }

            return View(loan);
        }

        [HttpPost]
        public IActionResult DeleteLoanConfirmed(int id)
        {
            LoanMethods loanMethods = new LoanMethods();
            string error;
            int rowsAffected = loanMethods.DeleteLoan(id, out error);

            if (rowsAffected == 1)
            {
                return RedirectToAction("SelectLoans");  
            }
            else
            {
                ViewBag.Error = error;
                return View("Error");  
            }
        }
    }
}
