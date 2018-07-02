using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class GenreDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
    }
}