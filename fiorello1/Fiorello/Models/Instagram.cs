using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Models
{
    public class Instagram
    {
        public int Id { get; set; }
        [Required]
        public string ImageURL { get; set; }
    }
}
