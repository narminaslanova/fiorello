using Fiorello.DAL;
using Fiorello.Extensions;
using Fiorello.Helpers;
using Fiorello.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            ViewBag.SlideCount = _context.Sliders.Count();
            return View(_context.Sliders.ToList());
        }
        public IActionResult Create()
        {
            if (_context.Sliders.Count() >= 5)
            {
                return Content("Invalid action!");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {

            #region Create multiple files
            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();

            int count = _context.Sliders.Count();
            if (slider.Photos.Count() + count > 5)
            {
                ModelState.AddModelError("Photos", $"{5 - count} image can be uploaded");
                return View();
            }

            foreach (IFormFile photo in slider.Photos)
            {
                if (!photo.IsValidType("image/"))
                {
                    ModelState.AddModelError("Photos", $"{photo.FileName} is not image type.");
                    return View();
                }
                if (!photo.IsValidSize(200))
                {
                    ModelState.AddModelError("Photos", $"{photo.FileName} size should be less than 200kb!");
                    return View();
                }

                slider.ImageURL = await photo.SaveFileAsync(_env.WebRootPath, "img");
                Slider newSlider = new Slider();
                newSlider.ImageURL = await photo.SaveFileAsync(_env.WebRootPath, "img");
                await _context.Sliders.AddAsync(newSlider);
            }
            #endregion

            #region Create one file
            //if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
            //if (!slider.Photo.IsValidType("image/"))
            //{
            //    ModelState.AddModelError("Photo", "Please, select image type.");
            //    return View();
            //}
            //if (!slider.Photo.IsValidSize(200))
            //{
            //    ModelState.AddModelError("Photo", "Image size should be less than 200kb");
            //    return View();
            //}

            #region notdynamic

            //string fileName = Guid.NewGuid().ToString() + slider.Photo.FileName;
            //string root = Path.Combine(_env.WebRootPath, "img");
            //string resultPath = Path.Combine(root, fileName);


            //using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            //{
            //    await slider.Photo.CopyToAsync(fileStream);
            //}
            #endregion


            //slider.ImageURL = await slider.Photo.SaveFileAsync(_env.WebRootPath, "img");
            //await _context.Sliders.AddAsync(slider);
            #endregion
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            if (_context.Sliders.Count() == 1)
            {
                return Content("Invalid action!");
            }
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();

            Helper.DeleteFile(_env.WebRootPath, "img", slider.ImageURL);
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = await _context.Sliders.FindAsync(id);
            if(slider==null) return NotFound();
            return View(slider);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public async Task<IActionResult> UpdatePost(int? id, Slider slider)
        {
            if (id == null) return NotFound();
            if (slider == null) return NotFound();
            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();

            foreach (IFormFile photo in slider.Photos)
            {
                if (!photo.IsValidType("image/"))
                {
                    ModelState.AddModelError("Photos", "Please, select image type.");
                    return View();
                }
                if (!photo.IsValidSize(200))
                {
                    ModelState.AddModelError("Photos", "Image size should be less than 200kb");
                    return View();
                }

                slider.ImageURL = await photo.SaveFileAsync(_env.WebRootPath, "img");
                //await _context.Sliders.AddAsync(slider);
                _context.Sliders.Update(slider);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }

}
