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

        public IActionResult SelectMoviesAndLoans()
        {
            MovieMethods movieMethods = new MovieMethods();
            LoanMethods loanMethods = new LoanMethods();

            string movieError = "";
            string loanError = "";

            var movies = movieMethods.GetMovieList(out movieError);
            var loans = loanMethods.GetLoanList(out loanError);

            var viewModel = new MovieLoanViewModel
            {
                Movies = movies, 
                Loans = loans   
            };

            ViewBag.MovieError = movieError;
            ViewBag.LoanError = loanError;

            return View(viewModel);
        }
    }
}
