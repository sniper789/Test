using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movie list
        public ActionResult List()
        {
            //MovieListViewModel model = new MovieListViewModel();
            //model.MovieList = _context.Movies.Include(m => m.Genre).ToList();
            //return View(model);

            return View();
        }

        //public ActionResult Details(int id)
        //{
        //    Movie model = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
        //    return View(model);
        //}

        public ActionResult New(MovieFormViewModel viewModel)
        {
            var genres = _context.Genres.ToList();
            viewModel.Genres = genres;
            viewModel.Movie = new Movie();
            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var genres = _context.Genres.ToList();
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            MovieFormViewModel viewModel = new MovieFormViewModel
            {
                Genres = genres,
                Movie = movie
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(MovieFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var genres = _context.Genres.ToList();
                viewModel.Genres = genres;
                return View("MovieForm", viewModel);
            }

            if (viewModel.Movie.Id == 0)
                _context.Movies.Add(viewModel.Movie);
            else
            {
                Movie movieInDb = _context.Movies.Single(m => m.Id == viewModel.Movie.Id);

                movieInDb.Title = viewModel.Movie.Title;
                movieInDb.GenreId = viewModel.Movie.GenreId;
                movieInDb.NumberInStock = viewModel.Movie.NumberInStock;
                movieInDb.DateAdded = viewModel.Movie.DateAdded;
                movieInDb.ReleaseDate = viewModel.Movie.ReleaseDate;
            }

            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}