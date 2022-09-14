using System.Diagnostics;
using System.Threading.Tasks;
using FreeCourse.Web.Exceptions;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FreeCourse.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICatalogService _catalogService;
        
        public HomeController(ILogger<HomeController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCoursesAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (errorFeature != null && errorFeature.Error is UnAuthorizedException)
            {
                return RedirectToAction(nameof(AuthController.SignOut), "Auth");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Detail(string id)
        {
            return View(await _catalogService.GetByCourseIdAsync(id));
        }
    }
}