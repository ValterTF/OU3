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

        public IActionResult EditMovie(int id)
        {
            MovieMethods movieMethods = new MovieMethods();
            string error;
            Movie movie = movieMethods.GetMovieDetails(id, out error);

            if (movie == null)
            {
                ViewBag.Error = error;
                return View("Error");
            }

            return View(movie);
        }

        [HttpPost]
        public IActionResult EditMovie(Movie movie)
        {
            MovieMethods movieMethods = new MovieMethods();
            string error;

            int rowsAffected = movieMethods.UpdateMovie(movie, out error);

            if (rowsAffected == 1)
            {
                return RedirectToAction("SelectMovies"); 
            }
            else
            {
                ViewBag.Error = error; 
                return View(movie); 
            }
        }


        public IActionResult DeleteMovie(int id)
        {
            MovieMethods movieMethods = new MovieMethods();
            string errormsg;
            Movie movie = movieMethods.GetMovieDetails(id, out errormsg);

            if (movie == null)
            {
                ViewBag.Error = errormsg;
                return View("Error"); 
            }

            return View(movie);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            MovieMethods movieMethods = new MovieMethods();
            string errormsg;
            int result = movieMethods.DeleteMovie(id, out errormsg);

            if (result > 0)
            {
                return RedirectToAction("SelectMovies"); 
            }

            ViewBag.Error = errormsg;
            return View("Error"); 
        }

    }
}
