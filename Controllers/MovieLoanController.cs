using Microsoft.AspNetCore.Mvc;
using OU3.Models;

namespace OU3.Controllers
{
    public class MovieLoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SelectMoviesLoan()
        {
            MovieLoanMethods movieLoanMethods = new MovieLoanMethods();
            string error = "";
            var movieLoanList = movieLoanMethods.GetMoviesAndLoans(out error);

            ViewBag.Error = error;
            return View(movieLoanList);
        }
    }
}
