using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebSockets;
using VidlyStore.Dtos;
using VidlyStore.Models;

namespace VidlyStore.api
{
    public class RentalController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult CreateNewRentels(RentalDto rentalDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (rentalDto.MovieIds.Count == 0)
                return BadRequest("No Movies have been given.");

            var customer = _context.customers.SingleOrDefault(c => c.Id == rentalDto.CustomerId);

            if (customer == null)
                return BadRequest("Invalid Customer ID");

            var movies = _context.movies.Where(m => rentalDto.MovieIds.Contains(m.Id)).ToList();

            if (movies.Count != rentalDto.MovieIds.Count)
                return BadRequest("one or more Movies are Invalid.");

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");

                movie.NumberAvailable --;
                var rental = new Rental
                {   
                    Customer =customer,
                    Movie = movie,
                    DateRented = DateTime.Now,
                    DateReturned = rentalDto.DateReturned
                };
                _context.rental.Add(rental);
                
            }

            _context.SaveChanges();
            return Ok();
        }

    }
}
