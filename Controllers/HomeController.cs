using Microsoft.AspNetCore.Mvc;

namespace restaurante.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); 
        }
    }
}