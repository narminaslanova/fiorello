using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public string Text { get; set; }
        public string AuthorName { get; set; }
        public string Position { get; set; }
    }
}
