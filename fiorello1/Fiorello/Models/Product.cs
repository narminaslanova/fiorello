using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
