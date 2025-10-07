using BinaryOperations.Web.Models;
using BinaryOperations.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BinaryOperations.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBinaryOperationService _binaryService;

        public HomeController(IBinaryOperationService binaryService)
        {
            _binaryService = binaryService;
        }

        public IActionResult Index()
        {
            var model = new BinaryOperationModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(BinaryOperationModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Results = _binaryService.CalculateOperations(model.A, model.B);
                    model.HasResults = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al calcular las operaciones: {ex.Message}");
                }
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
