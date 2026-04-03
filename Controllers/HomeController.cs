using Microsoft.AspNetCore.Mvc;

namespace restaurante.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Isso vai procurar a pasta Views/Home/Index.cshtml
        }
    }
}