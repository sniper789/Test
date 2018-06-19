using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public List<MembershipType> MembershipTypes { get; set; }
        public Movie Movie { get; set; }
    }
}