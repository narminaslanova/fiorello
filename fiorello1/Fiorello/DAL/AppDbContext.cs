using Fiorello.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options){}
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Caption> Captions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Experts> Experts { get; set; }
        public DbSet<ExpertsCaption> ExpertsCaption { get; set; }
        public DbSet<BlogCaption> BlogCaption { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Instagram> Instagrams { get; set; }
        public DbSet<InstagramCaption> InstagramCaptions { get; set; }
        public DbSet<Bio> Bio { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
