using Fiorello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<Experts> Experts { get; set; }
        public Caption Caption { get; set; }
        public About About { get; set; }
        public ExpertsCaption ExpertsCaption { get; set; }
        public BlogCaption BlogCaption { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Instagram> Instagrams { get; set; }
        public InstagramCaption InstagramCaption { get; set; }


    }
}
