using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using VidlyStore.Dtos;
using VidlyStore.Models;

namespace VidlyStore.api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
       
        public IHttpActionResult GetMovie(string query =null)
        {
            var movieQuery = _context.movies.Include(c => c.Genre);
            if (!string.IsNullOrWhiteSpace(query))
            {
                movieQuery = movieQuery.Where(m => m.Name.Contains(query) && m.NumberAvailable != 0);
            }
            var MovieDto = movieQuery.ToList().Select(Mapper.Map<Movie, MovieDto>);
            return Ok(MovieDto);
        }
      
        public IHttpActionResult GetMovies(int id)
        {
            var movie = _context.movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }
        [Authorize(Roles = RoleName.Publishers + "," + RoleName.Admin + "," + RoleName.Managers)]
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = _context.movies.Add(Mapper.Map<MovieDto, Movie>(movieDto));
            movie.NumberAvailable = movie.NumberInStock;
          
            _context.SaveChanges();
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }
        [Authorize(Roles = RoleName.Publishers + "," + RoleName.Admin + "," + RoleName.Managers)]
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id , MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var movieInDb = _context.movies.SingleOrDefault(c => c.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(movieDto, movieInDb);
            _context.SaveChanges();
            return Ok();

        }
        [Authorize(Roles = RoleName.Publishers + "," + RoleName.Admin + "," + RoleName.Managers)]
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.movies.Remove(movie);
            _context.SaveChanges();
            return Ok();
        }

    }
}
