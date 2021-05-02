﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Models
{
    public class AppUser:IdentityUser
    { 
          //extend by addin fullname or any other columns
          [Required]
          public string Fullname { get; set; }
          
          public bool IsDeleted { get; set; }
    }
}
