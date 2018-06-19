using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

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
            MovieListViewModel model = new MovieListViewModel();
            model.MovieList = _context.Movies.Include(m => m.Genre).ToList();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            Movie model = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            return View(model);
        }
    }
}