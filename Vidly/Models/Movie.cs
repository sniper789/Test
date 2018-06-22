using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(255)]
        public string Title { get; set; }
     
        public Genre Genre { get; set; }

        [Required(ErrorMessage ="Genre is required.")]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Release Date is required.")]
        [Display(Name ="Release Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Date Added is required.")]
        [Display(Name = "Date Added")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; }

        [Required(ErrorMessage = "Number in Stock is required.")]
        [Display(Name = "Number in Stock")]
        [Range(0, 20)]
        public int NumberInStock { get; set; }

        public Movie()
        {
            Id = 0;
            DateAdded = DateTime.Now.Date;
            ReleaseDate = DateTime.Now.Date;
        }
    }   
}