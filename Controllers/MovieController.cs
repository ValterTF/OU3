using Microsoft.AspNetCore.Mvc;
using OU3.Models;

namespace OU3.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertMovie()
        {
            Movie movie = new Movie();
            MovieMethods movieMethods = new MovieMethods();
            int i = 0;
            string error = "";

            movie.Title = "New Movie";
            movie.Year = 2024;
            movie.Director = "ValterTF";
            movie.CopiesAvailable = 1;

            i = movieMethods.InsertMovie(movie, out error);
            ViewBag.Error = error;
            ViewBag.antal = i;

            return View();
        }

        public IActionResult SelectMovies()
        {
            List<Movie> movieList = new List<Movie>();
            MovieMethods movieMethods = new MovieMethods();
            string error = "";
            movieList = movieMethods.GetMovieList(out error);
            ViewBag.Error = error;
            return View(movieList);
        }
    }
}
