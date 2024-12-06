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

        public IActionResult FilterByDirector(string director)
        {
            MovieMethods movieMethods = new MovieMethods();
            string error;

            var movies = movieMethods.GetMovieList(out error);

            if (movies == null || !string.IsNullOrEmpty(error))
            {
                ViewBag.Error = error;
                return View("Error");
            }
            var filteredMovies = movies.Where(m => m.Director == director).ToList();

            var viewModel = new MovieLoanViewModel
            {
                Movies = filteredMovies,
            };

            return View("SelectMoviesAndLoans", viewModel);
        }
    }
}
