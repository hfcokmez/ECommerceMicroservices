using System;
using System.Threading.Tasks;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.Inputs;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CourseController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCoursesByUserIdAsync(_sharedIdentityService.UserId));
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput courseCreateInput)
        {
            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View();
            }

            courseCreateInput.UserId = _sharedIdentityService.UserId;
            await _catalogService.CreateCourseAsync(courseCreateInput);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string courseId)
        {
            var course = await _catalogService.GetByCourseIdAsync(courseId);

            if (course == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", course.Id);
            var courseUpdateInput = new CourseUpdateInput()
            {
                Id = course.Id,
                Name = course.Name,
                Price = course.Price,
                Feature = course.Feature,
                CategoryId = course.CategoryId,
                UserId = course.UserId,
                Picture = course.Picture,
                Description = course.Description
            };
            return View(courseUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput courseUpdateInput)
        {
            var categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", courseUpdateInput.Id);
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.UpdateCourseAsync(courseUpdateInput);
            return RedirectToAction(nameof(Index));
        }
    }
}