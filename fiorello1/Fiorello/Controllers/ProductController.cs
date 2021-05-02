using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Controllers
{
    //[Authorize]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        //[AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.ProductCount = _context.Products.Count();
            return View(_context.Products.Take(8).ToList());
        }

        //[AllowAnonymous]
        public IActionResult LoadMore(int skip)
        {
            List<Product> model = _context.Products.Skip(skip).Take(8).ToList();
            return PartialView("_ProductPartial", model);
            //return Json(_context.Products.ToList());
        }
        [Authorize(Roles ="Admin, Member")]
        public IActionResult Buy()
        {
            return Content("Buy");
        }
    }
}
