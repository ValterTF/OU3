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

            LoanMethods loanMethods = new LoanMethods();
            var loans = loanMethods.GetLoanList(out error);

            var viewModel = new MovieLoanViewModel
            {
                Movies = filteredMovies,
                Loans = loans
            };

            return View("SelectMoviesAndLoans", viewModel);
        }

        public IActionResult SearchMovies(string title)
        {
            MovieMethods movieMethods = new MovieMethods();
            string error;

            var movies = movieMethods.GetMovieList(out error);

            if (movies == null || !string.IsNullOrEmpty(error))
            {
                ViewBag.Error = error;
                return View("Error");
            }

            var filteredMovies = movies.Where(m => m.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

            LoanMethods loanMethods = new LoanMethods(); 
            var loans = loanMethods.GetLoanList(out error);

            if (loans == null || !string.IsNullOrEmpty(error))
            {
                ViewBag.Error = error;
                return View("Error");
            }

            var viewModel = new MovieLoanViewModel
            {
                Movies = filteredMovies,
                Loans = loans 
            };
            return View("SelectMoviesAndLoans", viewModel);
        }

    }
}
