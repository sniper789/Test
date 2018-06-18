using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie list
        public ActionResult List()
        {
            MovieListViewModel model = new MovieListViewModel();
            model.MovieList = new List<Models.Movie>()
            {
                new Movie { Id=1, Title="Tomb RAIDER" },
                new Movie { Id=2, Title="BLA BLA BLA!!!" },
                new Movie { Id=3, Title="Superman 2" }
            };

            return View(model);
        }
    }
}