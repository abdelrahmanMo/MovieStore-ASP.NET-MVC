using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VidlyStore.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter The Movie Name.")]
        [Display(Name = "Movie Name")]
        public string Name { get; set; }
       
        [Display(Name = "Release Date ")]
        [Required(ErrorMessage = "please Enter The Release Date.")]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }
        [Display(Name = "Number In Stock")]
        [Required(ErrorMessage = "please Enter The Number In Stock.")]
        [Range(1,20,ErrorMessage = "The field Number In Stock must be between 1 and 20.")]
        public byte NumberInStock { get; set; }

        public byte NumberAvailable { get; set; }

        
        public Genre Genre { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public byte GenreId  { get; set; }

        public string Title
        {
            get
            {
                if (this.Id !=0)
                {
                    return "Edit Movie";
                }

                return "New Movie";
            }
        }

    }
}