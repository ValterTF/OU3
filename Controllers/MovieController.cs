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
            return View(movie);  
        }

        [HttpPost]
        public IActionResult InsertMovie(string Title, string Director, int Year, int CopiesAvailable)
        {
            Movie movie = new Movie
            {
                Title = Title,
                Director = Director,
                Year = Year,
                CopiesAvailable = CopiesAvailable
            };

            MovieMethods movieMethods = new MovieMethods();
            string error = "";
            int i = movieMethods.InsertMovie(movie, out error);

            ViewBag.Error = error;
            ViewBag.Antal = i;

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
