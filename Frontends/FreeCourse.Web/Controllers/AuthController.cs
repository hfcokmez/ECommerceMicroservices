using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult SingIn()
        {
            return View();
        }
    }
}