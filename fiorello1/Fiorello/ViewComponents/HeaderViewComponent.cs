using Fiorello.DAL;
using Fiorello.Models;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        public HeaderViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async  Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.BasketCount = 0;
            if (Request.Cookies["basket"] != null)
            {
                List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                ViewBag.BasketCount = basket.Count; //for general count
                //ViewBag.BasketCount = basket.Sum(p => p.Count); //for count of all products count
            }
            Bio model = _context.Bio.FirstOrDefault();
            return View(await Task.FromResult(model));
        }
    }
}
