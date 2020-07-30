using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VidlyStore.Models;

namespace VidlyStore.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter The Movie Name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "please Enter The Release Date.")]
        public DateTime ReleaseDate { get; set; }
        public DateTime DateAdded { get; set; }
      
        [Required(ErrorMessage = "please Enter The Number In Stock.")]
        [Range(1, 20, ErrorMessage = "The field Number In Stock must be between 1 and 20.")]
        public byte NumberInStock { get; set; }
        [Required]
        public byte GenreId { get; set; }

        public GenreDto Genre { get; set; }
    }
}