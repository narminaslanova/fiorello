﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string ImageURL { get; set; }
        //[NotMapped, Required]
        //public IFormFile Photo { get; set; }
        [NotMapped, Required]
        public List<IFormFile> Photos { get; set; }
    }
}
