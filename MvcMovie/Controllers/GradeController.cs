using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class GradeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Grade model)
        {
            if (ModelState.IsValid)
            {
                model.FinalScore = model.A * 0.3 + model.B * 0.3 + model.C * 0.4;
                ViewBag.Result = $"Điểm cuối cùng: {model.FinalScore:F2}";
            }
            return View(model);
        }
    }
}
