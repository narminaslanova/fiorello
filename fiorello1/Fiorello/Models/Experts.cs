using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Models
{
    public class Experts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        [Required]
        public string ImageURL { get; set; }
    }
}
