using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using VidlyStore.Models;

namespace VidlyStore.Controllers
{
    [Authorize(Roles = RoleName.Publishers+","+RoleName.Admin+","+RoleName.Managers)]
    public class MoviesController : Controller
    {

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
            
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.Publishers) || User.IsInRole(RoleName.Admin) || User.IsInRole(RoleName.Managers))
                return View("List");
            return View("ReadOnlyList");
        }

        public ActionResult Detail(int id)
        {
            var movie = _context.movies.Include(c=>c.Genre).SingleOrDefault(c => c.Id == id);
            return View(movie);
        }
       
        public ActionResult New()
        {
            ViewBag.Genre = new SelectList(_context.genres.ToList(), "Id", "Name");
            var movie = new Movie();
            ViewBag.Title = movie.Title;
            movie = null;
            
            return View("MovieForm",movie);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
        //    ModelState.SetModelValue("id", new ValueProviderResult(movie.id, "", CultureInfo.InvariantCulture));

            if (!ModelState.IsValid )
            {
                ViewBag.Genre = new SelectList(_context.genres.ToList(), "Id", "Name");
                return View("MovieForm", movie);
                
            }

          
            if (movie.Id == 0)
            {
                
                movie.DateAdded = DateTime.Now;
                _context.movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.GenreId = movie.GenreId;
                
            }
            
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }
      
        public ActionResult Edit(int id)
        {
            var movie = _context.movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.Genre = new SelectList(_context.genres.ToList(), "Id", "Name");
                ViewBag.Title = movie.Title;
                return View("MovieForm",movie);
            }
            
        }
    }
}