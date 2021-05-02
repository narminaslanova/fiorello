using Fiorello.DAL;
using Fiorello.Models;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {   
            //session
            //HttpContext.Session.SetString("name", "Ferid");
            //cookie
            //Response.Cookies.Append("surname", "Memmedov", new CookieOptions { MaxAge = TimeSpan.FromMinutes(20) });
            HomeVM homeVM = new HomeVM
            {
                Sliders = _context.Sliders.ToList(),
                Caption = _context.Captions.FirstOrDefault(),
                Categories=_context.Categories.ToList(),
                Products=_context.Products.Include(p=>p.Category).ToList(),
                About=_context.About.FirstOrDefault(),
                Experts=_context.Experts.ToList(),
                ExpertsCaption=_context.ExpertsCaption.FirstOrDefault(),
                BlogCaption=_context.BlogCaption.FirstOrDefault(),
                Blogs=_context.Blogs.ToList(),
                Testimonials=_context.Testimonials.ToList(),
                Instagrams=_context.Instagrams.ToList(),
                InstagramCaption=_context.InstagramCaptions.FirstOrDefault()
            };
            return View(homeVM);
        }

        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null) return NotFound();
            Product product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            List<BasketVM> basket;
            if (Request.Cookies["basket"] != null)
            {
                basket= JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }
            BasketVM ExistProduct=basket.FirstOrDefault(p => p.Id == id);
            if (ExistProduct != null)
            {
                ExistProduct.Count += 1;
            }
            else
            {
                basket.Add(new BasketVM { Id = product.Id, Count = 1 });
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Basket()
        {
            List<BasketProductVM> basketProducts = new List<BasketProductVM>();
            if (Request.Cookies["basket"] != null)
            {
                List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                foreach (BasketVM product in basket)
                {
                    Product dbProduct = await _context.Products.FindAsync(product.Id);
                    basketProducts.Add(new BasketProductVM
                    {
                        Id = product.Id,
                        Count = product.Count,
                        Title = dbProduct.Title,
                        Price = dbProduct.Price,
                        ImageURL = dbProduct.ImageURL
                    });
                }
            }
            
            return View(basketProducts);
            //session
            //string name=HttpContext.Session.GetString("name");
            //cookie
            //string surname = Request.Cookies["surname"];
            //return Content(name+" "+surname);

        }
        public IActionResult RemoveBasket(int? id)
        {
            if (id == null) return NotFound();
            if (Request.Cookies["basket"] != null)
            {
                List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                BasketVM ExistBasket = basket.FirstOrDefault(p => p.Id == id);
                if (ExistBasket != null)
                {
                    basket.Remove(ExistBasket);
                }
                Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            }
            TempData["Warning"] = "Basket does not exist";
            return RedirectToAction(nameof(Basket));

        }
    }
}
